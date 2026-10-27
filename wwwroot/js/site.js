// ==========================================================================
// JAVASCRIPT - FERIA DE EMPRENDIMIENTO
// Autor: Samuel Cortés Guirales
// ==========================================================================

document.addEventListener("DOMContentLoaded", function () {
    // 1. Auto-cierre suave de alertas de notificación tras 7 segundos
    const alerts = document.querySelectorAll(".alert-dismissible");
    alerts.forEach(function (alert) {
        setTimeout(function () {
            const bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
            if (bsAlert) {
                bsAlert.close();
            }
        }, 7000);
    });

    // 2. Formateo automático a mayúsculas para el campo "Número de Stand" (Ej: a-01 -> A-01)
    const inputStand = document.getElementById("NumeroStand");
    if (inputStand) {
        inputStand.addEventListener("input", function (e) {
            this.value = this.value.toUpperCase();
        });
    }

    // 3. Tooltips de Bootstrap inicializados
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
});
