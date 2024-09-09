function validarForm() {
    let validoGuardar = true;

    document.querySelectorAll('#formRegistro input').forEach(function(input) {
        if (input.value === '' || (input.type === 'email' && !validarCorreo(input.value))) {
            validoGuardar = false;
        }
    });

    document.getElementById('btnRegistro').disabled = !validoGuardar;
}

function validarCorreo(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(String(email).toLowerCase());
}

document.addEventListener('DOMContentLoaded', function() {
    document.querySelectorAll('#formRegistro input').forEach(function(input) {
        input.addEventListener('input', validarForm);
    });
});
