const MAX_DATA = 20;
const temperatureCValue = [];
const humidityValue = [];
const methaneGasValue = [];
const hydrogenGasValue = [];
const smokeValue = [];
const lpgGasValue = [];
const alcohonGasValue = [];
const xValue = [];
const yValue = [];
const zValue = [];
const addAtValue = [];

$.get("/arduino?limit=20", function (datas) {
    datas.forEach(function (data) {
        temperatureCValue.push(data.temperatureC)
        humidityValue.push(data.humidity)
        methaneGasValue.push(data.methaneGas)
        hydrogenGasValue.push(data.hydrogenGas)
        smokeValue.push(data.smokeValue)
        lpgGasValue.push(data.lpgGasValue)
        alcohonGasValue.push(data.alcohonGasValue)
        xValue.push(data.x)
        yValue.push(data.y)
        zValue.push(data.z)
        addAtValue.push(data.addAt)
    });
});

const ctxTemperatureC = document.getElementById("chart-TemperatureC").getContext("2d");
const ctxHumidity = document.getElementById("chart-Humidity").getContext("2d");
const ctxMethaneGas = document.getElementById("chart-MethaneGas").getContext("2d");
const ctxHydrogenGas = document.getElementById("chart-HydrogenGas").getContext("2d");
const ctxSmoke = document.getElementById("chart-Smoke").getContext("2d");
const ctxLpgGas = document.getElementById("chart-LpgGas").getContext("2d");
const ctxAlcohonGas = document.getElementById("chart-AlcohonGas").getContext("2d");
const ctxX = document.getElementById("chart-X").getContext("2d");
const ctxY = document.getElementById("chart-Y").getContext("2d");
const ctxZ = document.getElementById("chart-Z").getContext("2d");

const chartTemperatureC = new Chart(ctxTemperatureC, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'TemperatureC',
                data: temperatureCValue,
                borderColor: 'green',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: -45,             // batas bawah sumbu Y
                max: 85,           // batas atas sumbu Y
                ticks: {
                    stepSize: 10      // jarak antar nilai Y (0, 20, 40, 60, 80, 100)
                }
            }
        }
    }
});
const chartHumidity = new Chart(ctxHumidity, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'Humidity',
                data: humidityValue,
                borderColor: 'blue',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: 0,             // batas bawah sumbu Y
                max: 100,           // batas atas sumbu Y
                ticks: {
                    stepSize: 10      // jarak antar nilai Y (0, 20, 40, 60, 80, 100)
                }
            }
        }
    }
});
const chartMethaneGas = new Chart(ctxMethaneGas, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'MethaneGas',
                data: methaneGasValue,
                borderColor: 'green',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: 45,             // batas bawah sumbu Y
                max: 6900000
            }
        }
    }
});
const chartHydrogenGas = new Chart(ctxHydrogenGas, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'HydrogenGas',
                data: hydrogenGasValue,
                borderColor: 'blue',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: 40,             // batas bawah sumbu Y
                max: 18400000
            }
        }
    }
});
const chartSmoke = new Chart(ctxSmoke, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'Smoke',
                data: smokeValue,
                borderColor: 'green',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: 50,             // batas bawah sumbu Y
                max: 72700000
            }
        }
    }
});
const chartLpgGas = new Chart(ctxLpgGas, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'LpgGas',
                data: lpgGasValue,
                borderColor: 'blue',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: 50,             // batas bawah sumbu Y
                max: 13300000
            }
        }
    }
});
const chartAlcohonGas = new Chart(ctxAlcohonGas, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'AlcohonGas',
                data: alcohonGasValue,
                borderColor: 'green',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: 50,             // batas bawah sumbu Y
                max: 41000000
            }
        }
    }
});
const chartX = new Chart(ctxX, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'X',
                data: xValue,
                borderColor: 'blue',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: 0,             // batas bawah sumbu Y
                max: 66000,           // batas atas sumbu Y
                ticks: {
                    stepSize: 1200      // jarak antar nilai Y (0, 20, 40, 60, 80, 100)
                }
            }
        }
    }
});
const chartY = new Chart(ctxY, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'Y',
                data: yValue,
                borderColor: 'Green',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: 0,             // batas bawah sumbu Y
                max: 66000,           // batas atas sumbu Y
                ticks: {
                    stepSize: 1200      // jarak antar nilai Y (0, 20, 40, 60, 80, 100)
                }
            }
        }
    }
});
const chartZ = new Chart(ctxZ, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'Z',
                data: zValue,
                borderColor: 'blue',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {
        animation: false,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: false,  // selalu mulai dari 0
                min: 0,             // batas bawah sumbu Y
                max: 66000,           // batas atas sumbu Y
                ticks: {
                    stepSize: 1200      // jarak antar nilai Y (0, 20, 40, 60, 80, 100)
                }
            }
        }
    }
});



chartTemperatureC.update();
chartHumidity.update();
chartMethaneGas.update();
chartHydrogenGas.update();
chartSmoke.update();
chartLpgGas.update();
chartAlcohonGas.update();
chartX.update();
chartY.update();
chartZ.update();
//function addData(value) {
//    const now = new Date().toLocaleTimeString();

//    addAtValue.push(now);
//    temperatureCValue.push(value);
//    humidityValue.push(value);
//    methaneGasValue.push(value);
//    hydrogenGasValue.push(value);
//    smokeValue.push(value);
//    lpgGasValue.push(value);
//    alcohonGasValue.push(value);
//    xValue.push(value);
//    yValue.push(value);
//    zValue.push(value);

//    // Jika data melebihi batas, hapus yang paling awal
//    if (addAtValue.length > MAX_DATA) {
//        addAtValue.shift();
//        temperatureCValue.shift();
//        humidityValue.shift();
//        methaneGasValue.shift();
//        hydrogenGasValue.shift();
//        smokeValue.shift();
//        lpgGasValue.shift();
//        alcohonGasValue.shift();
//        xValue.shift();
//        yValue.shift();
//        zValue.shift();
//    }

//    chartTemperatureC.update();
//    chartHumidity.update();
//    chartMethaneGas.update();
//    chartHydrogenGas.update();
//    chartSmoke.update();
//    chartLpgGas.update();
//    chartAlcohonGas.update();
//    chartX.update();
//    chartY.update();
//    chartZ.update();
//}

// Simulasi data baru tiap 1 detik
//setInterval(() => {
//    const randomValue = Math.floor(Math.random() * 100);
//    addData(randomValue);
//}, 100);


const webSocket = new WebSocket('ws://protect.tryasp.net/ws/monitor');

// 1. (onopen): Dipanggil saat koneksi berhasil dibuka
webSocket.onopen = function (event) {
    console.log('Koneksi berhasil dibuka!');

    // Setelah koneksi terbuka, kita bisa mulai mengirim data
    const data = {
        ok: "ok"
    }
    console.log(data);
    webSocket.send(JSON.stringify(data));
};

// 2. (onmessage): Dipanggil setiap kali menerima pesan dari server
webSocket.onmessage = function (event) {
    //console.log("Raw data dari server:", event.data);

    try {
        const objData = JSON.parse(event.data);
        console.log(objData.AddAt);
        console.log(objData.TemperatureC);
        console.log(objData.Humidity);
        console.log(objData.MethaneGas);
        console.log(objData.HydrogenGas);
        console.log(objData.Smoke);
        console.log(objData.LpgGas);
        console.log(objData.AlcohonGas);
        console.log(objData.X);
        console.log(objData.Y);
        console.log(objData.Z);

        addAtValue.push(objData.AddAt);
        temperatureCValue.push(objData.TemperatureC);
        humidityValue.push(objData.Humidity);
        methaneGasValue.push(objData.MethaneGas);
        hydrogenGasValue.push(objData.HydrogenGas);
        smokeValue.push(objData.Smoke);
        lpgGasValue.push(objData.LpgGas);
        alcohonGasValue.push(objData.AlcohonGas);
        xValue.push(objData.X);
        yValue.push(objData.Y);
        zValue.push(objData.Z);

        // Jika data melebihi batas, hapus yang paling awal
        if (addAtValue.length > MAX_DATA) {
            addAtValue.shift();
            temperatureCValue.shift();
            humidityValue.shift();
            methaneGasValue.shift();
            hydrogenGasValue.shift();
            smokeValue.shift();
            lpgGasValue.shift();
            alcohonGasValue.shift();
            xValue.shift();
            yValue.shift();
            zValue.shift();
        }

        // Update semua chart
        chartTemperatureC.update();
        chartHumidity.update();
        chartMethaneGas.update();
        chartHydrogenGas.update();
        chartSmoke.update();
        chartLpgGas.update();
        chartAlcohonGas.update();
        chartX.update();
        chartY.update();
        chartZ.update();

    } catch (err) {
        console.error("❌ Gagal parse JSON:", err, "Data:", event.data);
    }
};


// 3. (onclose): Dipanggil saat koneksi ditutup (baik oleh server maupun client)
webSocket.onclose = function (event) {
    if (event.wasClean) {
        console.log(`Koneksi ditutup dengan bersih, kode=${event.code} alasan=${event.reason}`);
    } else {
        console.log('Koneksi terputus (error)');
    }
};

// 4. (onerror): Dipanggil saat terjadi error koneksi
webSocket.onerror = function (error) {
    console.log(`[error] Terjadi error: ${error.message}`);
};

let i = 1;
const dangerContainer = document.getElementById('danger-buttor');
dangerContainer.addEventListener('click', function (event) {
    if (webSocket.readyState === WebSocket.OPEN) {
        if (i % 2 == 0) {
            webSocket.send("danger");
        }
        else {
            webSocket.send("secure");
        }
        i++;
    }
    $.ajax({
        url: '/arduino',
        type: 'DELETE',
        success: function (response) {
            console.log('Resource deleted successfully:', response);
        },
        error: function (xhr, status, error) {
            console.error('Error deleting resource:', error);
        }
    });
});