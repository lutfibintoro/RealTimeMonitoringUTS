// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// JavaScript yang lebih aman
function initDangerButtonAnimation() {
    const dangerButton = document.getElementById('danger-button');

    if (!dangerButton) return;

    // Hapus event listener sebelumnya jika ada
    dangerButton.removeEventListener('click', handleDangerClick);
    dangerButton.addEventListener('click', handleDangerClick);
}

function handleDangerClick(e) {
    e.preventDefault();
    const button = e.currentTarget;

    // Hapus class animasi sebelumnya
    button.classList.remove('danger-click-animation');

    // Trigger reflow
    void button.offsetWidth;

    // Tambahkan class animasi
    button.classList.add('danger-click-animation');

    // Buat efek partikel
    createDangerParticles(button);

    // Hapus class animasi setelah selesai
    setTimeout(() => {
        button.classList.remove('danger-click-animation');
    }, 600);
}

function createDangerParticles(button) {
    const buttonRect = button.getBoundingClientRect();
    const particlesContainer = document.createElement('div');
    particlesContainer.className = 'danger-particles-container';
    document.body.appendChild(particlesContainer);

    for (let i = 0; i < 12; i++) {
        const particle = document.createElement('div');
        particle.className = 'danger-particle';

        const angle = (i / 12) * Math.PI * 2;
        const distance = 50 + Math.random() * 50;
        const tx = Math.cos(angle) * distance;
        const ty = Math.sin(angle) * distance;

        particle.style.setProperty('--danger-tx', `${tx}px`);
        particle.style.setProperty('--danger-ty', `${ty}px`);

        const hue = 350 + Math.random() * 20;
        particle.style.background = `hsl(${hue}, 100%, 65%)`;

        const size = 4 + Math.random() * 6;
        particle.style.width = `${size}px`;
        particle.style.height = `${size}px`;

        particle.style.left = `${buttonRect.left + buttonRect.width / 2}px`;
        particle.style.top = `${buttonRect.top + buttonRect.height / 2}px`;

        particlesContainer.appendChild(particle);
    }

    setTimeout(() => {
        particlesContainer.remove();
    }, 1000);
}

// Inisialisasi saat dokumen ready
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initDangerButtonAnimation);
} else {
    initDangerButtonAnimation();
}