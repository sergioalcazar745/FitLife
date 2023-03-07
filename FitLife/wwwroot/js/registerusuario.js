$(document).ready(function () {

    $("#cliente").click(function () {
        $("#content-edad").show();
        $("#content-sexo").show();
        $("#content-altura").show();
        $("#content-peso").show();
    })

    $("#entrenador").click(function () {
        $("#content-edad").hide();
        $("#content-sexo").hide();
        $("#content-altura").hide();
        $("#content-peso").hide();
    })

    $("#nutricionista").click(function () {
        $("#content-edad").hide();
        $("#content-sexo").hide();
        $("#content-altura").hide();
        $("#content-peso").hide();
    })

    $("#buttonregister").click(function () {
        var correcto = true;
        var correctoEmail = true;
        var correctoDni = true;
        var nombre = $("#nombre");
        var apellidos = $("#apellidos");
        var dni = $("#dni");
        var email = $("#email");
        var password = $("#password");
        var repeatpassword = $("#repeatpassword");

        let emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/

        if (nombre.val() == "") {
            nombre.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            $("#error-nombre").remove();
            $("#content-nombre").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-nombre">El nombre es obligatorio</p>')
            correcto = false;
            console.log("Nombre")
        } else {
            if ($("#error-nombre").length) {
                $("#error-nombre").remove();
                nombre.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
            }
        }

        if (apellidos.val() == "") {
            apellidos.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            $("#error-apellidos").remove();
            $("#content-apellidos").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-apellidos">Los apellidos son obligatorios</p>')
            correcto = false;
            console.log("Apellidos")
        } else {
            if ($("#error-apellidos").length) {
                $("#error-apellidos").remove();
                apellidos.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
            }
        }

        if (dni.val() == "") {
            dni.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            $("#error-dni").remove();
            $("#content-dni").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-dni">El dni es obligatorio</p>')
            correcto = false;
            correctoDni = false;
            console.log("DNI")
        } else {
            if ($("#error-dni").length) {
                $("#error-dni").remove();
                dni.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
            }
        }

        if (correctoDni) {
            if (!validateDNI(dni.val())) {
                dni.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
                $("#error-dni").remove();
                $("#content-dni").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-dni">Formato incorrecto</p>')
                correcto = false;
                console.log("Email")
            } else {
                if ($("#error-dni").length) {
                    $("#error-dni").remove();
                    dni.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
                }
            }
        }

        if (email.val() == "") {
            email.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            $("#error-email").remove();
            $("#content-email").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-email">El email es obligatorio</p>')
            correcto = false;
            correctoEmail = false;
            console.log("Email")
        } else {
            if ($("#error-email").length) {
                $("#error-email").remove();
                email.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
            }
        }

        if (correctoEmail) {
            if (!emailRegex.test(email.val())) {
                email.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
                $("#error-email").remove();
                $("#content-email").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-email">Formato incorrecto</p>')
                correcto = false;
                console.log("Email")
            } else {
                if ($("#error-email").length) {
                    $("#error-email").remove();
                    email.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
                }
            }
        }

        if (password.val() == "") {
            password.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            $("#error-password").remove();
            $("#content-password").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-password">La password es obligatorio</p>')
            correcto = false;
            console.log("Password")
        } else {
            $("#error-password").remove();
            password.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
        }

        // if (repeatpassword.val() == "") {
        //     repeatpassword.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
        //     $("#error-repeatpassword").remove();
        //     $("#content-repeatpassword").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-repeatpassword">Repetir la contraseña es obligatorio</p>')
        //     correcto = false;
        //     console.log("RepeatPassword")
        // }else{
        //     if($("#error-repeatpassword").length){
        //         $("#error-repeatpassword").remove();
        //         repeatpassword.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
        //     }
        // }

        if (password.val() != repeatpassword.val()) {
            repeatpassword.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            password.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            $("#error-repeatpassword").remove();
            $("#content-repeatpassword").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-repeatpassword">Las contraseñas no coinciden</p>')
            correcto = false;
            console.log("Desigual")
        } else {
            if ($("#error-repeatpassword").length) {
                $("#error-repeatpassword").remove();
                repeatpassword.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
            }
        }

        console.log("Checked" + $("#cliente:checked").is(":checked"))

        if ($("#cliente:checked").is(":checked")) {
            var edad = $("#edad");
            var altura = $("#altura")
            var peso = $("#peso")

            if (edad.val() == "") {
                edad.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600 dark:text-red-900")
                $("#error-edad").remove();
                $("#content-edad").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-edad">La edad es obligatoria</p>')
                correcto = false;
                console.log("Edad")
            } else {
                if ($("#error-edad").length) {
                    $("#error-edad").remove();
                    edad.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
                }
            }

            if (altura.val() == "") {
                altura.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600 dark:text-red-900")
                $("#error-altura").remove();
                $("#content-altura").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-altura">La altura es obligatoria</p>')
                correcto = false;
                console.log("Altura")
            } else {
                if ($("#error-altura").length) {
                    $("#error-altura").remove();
                    altura.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
                }
            }

            if (peso.val() == "") {
                peso.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600 dark:text-red-900")
                $("#error-peso").remove();
                $("#content-peso").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-peso">La Peso es obligatoria</p>')
                correcto = false;
                console.log("Peso")
            } else {
                if ($("#error-peso").length) {
                    $("#error-peso").remove();
                    peso.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
                }
            }
        }

        // console.log("Correcto: " + correcto)

        if (correcto) {
            $("#alert-error").hide();
            $("#form").submit();
        } else {
            $("#alert-error").remove();
            $("#content-form").before(`<div class="flex p-4 mb-4 text-sm text-red-800 rounded-lg bg-red-50 dark:bg-red-800 dark:text-red-400" role="alert" id="alert-error" hidden>
                <svg aria-hidden="true" class="flex-shrink-0 inline w-5 h-5 mr-3" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z" clip-rule="evenodd"></path></svg>
                <span class="sr-only">Info</span>
                <div>
                    <span class="font-medium text-white">Existen campos incorrectos, revíselos.</span>
                </div>
            </div>`);
            $(window).scrollTop(0);
            console.log("-------------------")
        }
    })

    function validateDNI(dni) {
        var numero, let, letra;
        var expresion_regular_dni = /^[XYZ]?\d{5,8}[A-Z]$/;

        dni = dni.toUpperCase();

        if (expresion_regular_dni.test(dni) === true) {
            numero = dni.substr(0, dni.length - 1);
            numero = numero.replace('X', 0);
            numero = numero.replace('Y', 1);
            numero = numero.replace('Z', 2);
            let = dni.substr(dni.length - 1, 1);
            numero = numero % 23;
            letra = 'TRWAGMYFPDXBNJZSQVHLCKET';
            letra = letra.substring(numero, numero + 1);
            if (letra != let) {
                //alert('Dni erroneo, la letra del NIF no se corresponde');
                return false;
            } else {
                //alert('Dni correcto');
                return true;
            }
        } else {
            //alert('Dni erroneo, formato no válido');
            return false;
        }
    }
})

    