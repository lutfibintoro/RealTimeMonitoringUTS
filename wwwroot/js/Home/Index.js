// ==================== Utility Functions ====================
function random(min, max) {
    return (Math.random() * (max - min) + min).toFixed(2);
}

// ==================== Date and Time Display ====================
function updateTime() {
    const now = new Date();
    const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

    document.getElementById("clock").innerText = now.toLocaleTimeString('en-GB');
    document.getElementById("date").innerText = now.toLocaleDateString('id-ID', options);
}

setInterval(updateTime, 1000);
updateTime();

// ==================== Chart Configuration ====================
const cartoonOptions = {
    animation: {
        duration: 1500,
        easing: 'easeOutBounce'
    },
    plugins: {
        legend: {
            display: true,
            position: 'top',
            labels: {
                font: {
                    size: 14,
                    family: 'Comic Sans MS'
                }
            }
        }
    }
};

// ==================== Chart Initialization ====================

// Temperature Line Chart
const ctxTemp = document.getElementById('temperatureChart').getContext('2d');
const temperatureChart = new Chart(ctxTemp, {
    type: 'line',
    data: {
        labels: ['Helmet-1', 'Helmet-2', 'Helmet-3', 'Helmet-4', 'Helmet-5', 'Helmet-6'],
        datasets: [{
            label: 'Temperatur (°C)',
            data: [],
            borderColor: '#FF6B6B',
            backgroundColor: 'rgba(255, 107, 107, 0.3)',
            borderWidth: 3,
            fill: true,
            tension: 0.4,
            pointBackgroundColor: '#FF6B6B',
            pointBorderColor: '#fff',
            pointBorderWidth: 3,
            pointRadius: 8,
        }]
    },
    options: {
        ...cartoonOptions,
        scales: {
            y: {
                beginAtZero: false,
                min: 25,
                max: 40
            }
        }
    }
});

// Humidity Bar Chart
const ctxHum = document.getElementById('humidityChart').getContext('2d');
const humidityChart = new Chart(ctxHum, {
    type: 'bar',
    data: {
        labels: ['Helmet-1', 'Helmet-2', 'Helmet-3', 'Helmet-4', 'Helmet-5', 'Helmet-6'],
        datasets: [{
            label: 'Humidity (%)',
            data: [],
            backgroundColor: ['#4ECDC4', '#45B7D1', '#96CEB4', '#FFEAA7', '#DDA0DD', '#98D8C8'],
            borderColor: '#fff',
            borderWidth: 2,
        }]
    },
    options: cartoonOptions
});

// Status Pie Chart
const ctxStatus = document.getElementById('statusChart').getContext('2d');
const statusChart = new Chart(ctxStatus, {
    type: 'pie',
    data: {
        labels: ['Online', 'Offline'],
        datasets: [{
            data: [1, 5],
            backgroundColor: ['#00C853', '#E53935'],
            borderColor: '#fff',
            borderWidth: 3,
        }]
    },
    options: cartoonOptions
});

// Methane Doughnut Chart
const ctxMeth = document.getElementById('methaneChart').getContext('2d');
const methaneChart = new Chart(ctxMeth, {
    type: 'doughnut',
    data: {
        labels: ['Helmet-1', 'Helmet-2', 'Helmet-3', 'Helmet-4', 'Helmet-5', 'Helmet-6'],
        datasets: [{
            label: 'Metana (ppm)',
            data: [],
            backgroundColor: ['#FF9FF3', '#FECA57', '#FF6B6B', '#48DBFB', '#0ABDE3', '#10AC84'],
            borderColor: '#fff',
            borderWidth: 3,
        }]
    },
    options: cartoonOptions
});

// Acceleration Radar Chart
const ctxAcc = document.getElementById('accelerationChart').getContext('2d');
const accelerationChart = new Chart(ctxAcc, {
    type: 'radar',
    data: {
        labels: ['Helmet-1', 'Helmet-2', 'Helmet-3', 'Helmet-4', 'Helmet-5', 'Helmet-6'],
        datasets: [{
            label: 'Akselerasi X',
            data: [],
            backgroundColor: 'rgba(255, 99, 132, 0.3)',
            borderColor: '#FF6384',
            borderWidth: 3,
            pointBackgroundColor: '#FF6384',
            pointBorderColor: '#fff',
            pointRadius: 6,
        }, {
            label: 'Akselerasi Y',
            data: [],
            backgroundColor: 'rgba(54, 162, 235, 0.3)',
            borderColor: '#36A2EB',
            borderWidth: 3,
            pointBackgroundColor: '#36A2EB',
            pointBorderColor: '#fff',
            pointRadius: 6,
        }, {
            label: 'Akselerasi Z',
            data: [],
            backgroundColor: 'rgba(75, 192, 192, 0.3)',
            borderColor: '#4BC0C0',
            borderWidth: 3,
            pointBackgroundColor: '#4BC0C0',
            pointBorderColor: '#fff',
            pointRadius: 6,
        }]
    },
    options: cartoonOptions
});

// Gas Levels Area Chart
const ctxGas = document.getElementById('gasChart').getContext('2d');
const gasChart = new Chart(ctxGas, {
    type: 'line',
    data: {
        labels: ['Helmet-1', 'Helmet-2', 'Helmet-3', 'Helmet-4', 'Helmet-5', 'Helmet-6'],
        datasets: [{
            label: 'Smoke (ppm)',
            data: [],
            borderColor: '#FF9F40',
            backgroundColor: 'rgba(255, 159, 64, 0.3)',
            borderWidth: 3,
            fill: true,
            tension: 0.4,
            pointBackgroundColor: '#FF9F40',
            pointBorderColor: '#fff',
            pointRadius: 8,
        }, {
            label: 'LPG Gas (ppm)',
            data: [],
            borderColor: '#FF6384',
            backgroundColor: 'rgba(255, 99, 132, 0.3)',
            borderWidth: 3,
            fill: true,
            tension: 0.4,
            pointBackgroundColor: '#FF6384',
            pointBorderColor: '#fff',
            pointRadius: 8,
        }]
    },
    options: {
        ...cartoonOptions,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

// ==================== Update Charts Function ====================
function updateCharts(temps, hums, mets, smks, lpgs, ax, ay, az) {
    temperatureChart.data.datasets[0].data = temps;
    temperatureChart.update('active');

    humidityChart.data.datasets[0].data = hums;
    humidityChart.update('active');

    statusChart.update('active');

    methaneChart.data.datasets[0].data = mets;
    methaneChart.update('active');

    accelerationChart.data.datasets[0].data = ax;
    accelerationChart.data.datasets[1].data = ay;
    accelerationChart.data.datasets[2].data = az;
    accelerationChart.update('active');

    gasChart.data.datasets[0].data = smks;
    gasChart.data.datasets[1].data = lpgs;
    gasChart.update('active');
}

// ==================== Load Helmet Data ====================
function loadHelmetData() {
    const workerNames = ["Ahmad", "Budi", "Citra", "Dedi", "Eka", "Fajar"];
    const locations = ["Pos1", "Pos2", "Pos3", "Pos4", "Pos5", "Pos6"];

    let table = "";
    let temperatures = [];
    let humidities = [];
    let methanes = [];
    let smokes = [];
    let lpgs = [];
    let accX = [];
    let accY = [];
    let accZ = [];

    for (let i = 1; i <= 6; i++) {
        let temp = parseFloat(random(29, 37));
        let hum = parseFloat(random(40, 90));
        let met = parseFloat(random(100, 500));
        let smk = parseFloat(random(10, 100));
        let lpg = parseFloat(random(50, 300));
        let ax = parseFloat(random(-1, 1));
        let ay = parseFloat(random(-1, 1));
        let az = parseFloat(random(-1, 1));

        temperatures.push(temp);
        humidities.push(hum);
        methanes.push(met);
        smokes.push(smk);
        lpgs.push(lpg);
        accX.push(ax);
        accY.push(ay);
        accZ.push(az);

        let rowClass = temp > 35 ? "high-temp" : "";
        table += `
		<tr class="${rowClass}">
			<td>Helmet-${i}</td>
			<td>${workerNames[i - 1]}</td>
			<td>${locations[i - 1]}</td>
			<td>${temp}</td>
			<td>${hum}</td>
			<td>${met} ppm</td>
			<td>${random(50, 400)} ppm</td>
			<td>${smk} ppm</td>
			<td>${lpg} ppm</td>
			<td>${random(20, 250)} ppm</td>
			<td>${ax}</td>
			<td>${ay}</td>
			<td>${az}</td>
		</tr>`;
    }

    document.getElementById("helmetData").innerHTML = table;
    updateCharts(temperatures, humidities, methanes, smokes, lpgs, accX, accY, accZ);
}

// ==================== Initialize and Update Data ====================
setInterval(loadHelmetData, 3000);
loadHelmetData();