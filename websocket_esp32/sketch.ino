#include <Arduino.h>
#include <WiFi.h>
#include <WiFiMulti.h>
#include <WebSocketsClient.h>
#include <ArduinoJson.h>
#include <DHT.h>
#include <Wire.h>
#include <MPU6050.h>

const char *ssid = "Wokwi-GUEST";
const char *pass = "";
const char *websocket_server = "protect.tryasp.net";
const int websocket_port = 80;
const char *websocket_path = "/ws/monitor";

WiFiMulti wiFiMulti;
WebSocketsClient webSocket;
String myGetMessage;

const int buzzerPin = 26;

void webSocketEvent(WStype_t type, uint8_t *payload, size_t length)
{
    switch (type)
    {
    case WStype_DISCONNECTED:
        Serial.printf("[WSc] Disconnected!\n");
        break;
    case WStype_CONNECTED:
        Serial.printf("[WSc] Connected to url: %s\n", payload);
        // Send connection message if needed
        webSocket.sendTXT("{\"type\":\"connected\",\"device\":\"esp32\"}");
        break;
    case WStype_TEXT:
        Serial.printf("[WSc] Received text: %s\n", payload);

        // Handle incoming messages from server
        myGetMessage = String((char *)payload);
        if (myGetMessage == "danger")
        {
            tone(buzzerPin, 2093);
            Serial.printf("danger area");
            delay(100);
        }
        else if (myGetMessage == "secure")
        {
            noTone(buzzerPin);
            Serial.printf("secure area");
            delay(100);
        }
        break;
    case WStype_ERROR:
        Serial.printf("[WSc] Error: %s\n", payload);
        break;
    case WStype_PING:
        Serial.printf("[WSc] Got ping\n");
        break;
    case WStype_PONG:
        Serial.printf("[WSc] Got pong\n");
        break;
    }
}

// Sensor definitions
#define DHTPIN 4
#define DHTTYPE DHT22
DHT dht(DHTPIN, DHTTYPE);

#define MQ2_PIN 34
float Ro = 10.0;

float MQResistanceCalculation(int raw_adc)
{
    return (4095.0 / raw_adc - 1.0) * Ro;
}

float MQRatio(float rs_ro_ratio, float a, float b)
{
    return a * pow(rs_ro_ratio, b);
}

MPU6050 mpu;
int16_t ax_prev = 0, ay_prev = 0, az_prev = 0;
#define LED_PIN 25

void sendSensorData()
{
    // Baca data sensor
    float kelembapan = dht.readHumidity();
    float temperature = dht.readTemperature();

    // MQ2
    int raw_adc = analogRead(MQ2_PIN);
    float Rs = MQResistanceCalculation(raw_adc);
    float ratio = Rs / Ro;
    float lpg_ppm = MQRatio(ratio, 1000.0, -2.2);
    float methane_ppm = MQRatio(ratio, 800.0, -2.1);
    float hydrogen_ppm = MQRatio(ratio, 900.0, -2.3);
    float smoke_ppm = MQRatio(ratio, 1500.0, -2.5);
    float alcohol_ppm = MQRatio(ratio, 1300.0, -2.4);

    // MPU6050
    int16_t ax, ay, az;
    mpu.getAcceleration(&ax, &ay, &az);
    int deltaX = abs(ax - ax_prev);
    int deltaY = abs(ay - ay_prev);
    int deltaZ = abs(az - az_prev);

    ax_prev = ax;
    ay_prev = ay;
    az_prev = az;

    // Buat JSON data
    DynamicJsonDocument doc(512);
    doc["TemperatureC"] = isnan(temperature) ? 0 : temperature;
    doc["Humidity"] = isnan(kelembapan) ? 0 : kelembapan;
    doc["MethaneGas"] = methane_ppm;
    doc["HydrogenGas"] = hydrogen_ppm;
    doc["Smoke"] = smoke_ppm;
    doc["LpgGas"] = lpg_ppm;
    doc["AlcohonGas"] = alcohol_ppm;
    doc["X"] = deltaX;
    doc["Y"] = deltaY;
    doc["Z"] = deltaZ;

    // Serialize dan kirim
    String jsonString;
    serializeJson(doc, jsonString);

    if (webSocket.isConnected())
    {
        webSocket.sendTXT(jsonString);
        Serial.println("Data sent: " + jsonString);
    }
    else
    {
        Serial.println("WebSocket not connected, cannot send data");
    }
}

void setup()
{
    pinMode(LED_PIN, OUTPUT);
    pinMode(buzzerPin, OUTPUT);
    Serial.begin(115200);

    // Inisialisasi sensor
    Wire.begin(21, 22);
    mpu.initialize();
    if (mpu.testConnection())
    {
        Serial.println("MPU6050 berhasil terhubung!");
    }
    else
    {
        Serial.println("Gagal terhubung ke MPU6050!");
    }
    dht.begin();

    // Koneksi WiFi
    WiFi.begin(ssid, pass);
    Serial.print("Connecting to WiFi");
    while (WiFi.status() != WL_CONNECTED)
    {
        delay(500);
        Serial.print(".");
        digitalWrite(LED_PIN, !digitalRead(LED_PIN)); // Blink while connecting
    }
    Serial.println();
    Serial.printf("[SETUP] WiFi Connected %s\n", WiFi.localIP().toString().c_str());

    // Setup WebSocket
    Serial.println("Setting up WebSocket client");

    webSocket.begin(websocket_server, websocket_port, websocket_path);
    webSocket.onEvent(webSocketEvent);

    // Optional: set reconnect interval
    webSocket.setReconnectInterval(5000);

    // Optional: enable heartbeat (ping/pong)
    webSocket.enableHeartbeat(15000, 3000, 2);
}

unsigned long lastDataSend = 0;
const unsigned long dataInterval = 200; // Kirim data setiap 5 detik

void loop()
{
    webSocket.loop();

    // LED indicator
    digitalWrite(LED_PIN, HIGH);
    delay(100);
    digitalWrite(LED_PIN, LOW);
    delay(100);

    // Kirim data sensor secara periodik jika terkoneksi
    // if (webSocket.isConnected() && millis() - lastDataSend > dataInterval)
    // {
    //     sendSensorData();
    //     lastDataSend = millis();
    // }
    if (webSocket.isConnected())
    {
        sendSensorData();
        lastDataSend = millis();
    }
}