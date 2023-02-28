$(document).ready(function () {

    $("#cliente").click(function () {
        $("#content-edad").show();
        $("#content-sexo").show();
    })

    $("#entrenador").click(function () {
        $("#content-edad").hide();
        $("#content-sexo").hide();
    })

    $("#nutricionista").click(function () {
        $("#content-edad").hide();
        $("#content-sexo").hide();
    })

    $("#buttonregister").click(function () {
        var correcto = true;
        var name = $("#name");
        var lastname = $("#lastname");
        var email = $("#email");
        var password = $("#password");
        var repeatpassword = $("#repeatpassword");

        let emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/

        if (name.val() == "") {
            name.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            $("#error-name").remove();
            $("#content-name").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-name">El nombre es obligatorio</p>')
            correcto = false;
            console.log("Name")
        } else {
            if ($("#error-name").length) {
                $("#error-name").remove();
                name.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
            }
        }

        if (lastname.val() == "") {
            lastname.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            $("#error-lastname").remove();
            $("#content-lastname").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-lastname">Los apellidos son obligatorios</p>')
            correcto = false;
            console.log("LastName")
        } else {
            if ($("#error-lastname").length) {
                $("#error-lastname").remove();
                lastname.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
            }
        }

        if (email.val() == "") {
            email.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600")
            $("#error-email").remove();
            $("#content-email").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-email">El email es obligatorio</p>')
            correcto = false;
            console.log("Email")
        } else {
            if ($("#error-email").length) {
                $("#error-email").remove();
                email.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
            }
        }

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
            var edad = $("#age");
            var altura = $("#height")
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

            if (edad.val() == "") {
                altura.removeClass("border border-gray-300 dark:border-gray-600").addClass("border border-red-500 dark:border-red-600 dark:text-red-900")
                $("#error-height").remove();
                $("#content-height").append('<p class="mt-2 text-sm text-red-600 dark:text-red-500" id="error-height">La altura es obligatoria</p>')
                correcto = false;
                console.log("Altura")
            } else {
                if ($("#error-height").length) {
                    $("#error-height").remove();
                    altura.addClass("border border-gray-300 dark:border-gray-600").removeClass("border border-red-500 dark:border-red-600")
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
        }
    })
})