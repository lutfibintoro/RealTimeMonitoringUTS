const MAX_DATA = 20;
const temperatureCValue = [];
const humidityValue = [];
const methaneGasValue = [];
const hydrogenGasValue = [];
const addAtValue = [];

$.get("/arduino?limit=20", function (datas) {
    datas.forEach(function (data) {
        temperatureCValue.push(data.temperatureC)
        humidityValue.push(data.humidity)
        methaneGasValue.push(data.methaneGas)
        hydrogenGasValue.push(data.hydrogenGas)
        addAtValue.push(data.addAt)
    });
});

const ctxTemperatureC = document.getElementById("chart-TemperatureC").getContext("2d");
const ctxHumidity = document.getElementById("chart-Humidity").getContext("2d");
const ctxMethaneGas = document.getElementById("chart-MethaneGas").getContext("2d");
const ctxHydrogenGas = document.getElementById("chart-HydrogenGas").getContext("2d");

const chartTemperatureC = new Chart(ctxTemperatureC, {
    type: 'line',
    data: {
        labels: addAtValue,
        datasets: [
            {
                label: 'TemperatureC',
                data: temperatureCValue,
                borderColor: 'pink',
                borderWidth: 1,
                tension: 0.1,
                fill: false
            }
        ]
    },
    options: {

    }
});
const chartHumidity = new Chart(ctxHumidity, {});
const chartMethaneGas = new Chart(ctxMethaneGas, {});
const chartHydrogenGas = new Chart(ctxHydrogenGas, {});