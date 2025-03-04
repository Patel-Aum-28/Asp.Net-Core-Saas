// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


(function ($) {
    "use strict";

    // Email validation
    var emailInput = document.getElementById("Email");
    if (emailInput) {
        emailInput.addEventListener('input', function () {
            var emailValue = emailInput.value;
            var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            var emailFeedback = document.getElementById("email-feedback");
            if (!emailPattern.test(emailValue)) {
                emailFeedback.style.color = "red";
                emailFeedback.innerText = "Please enter a valid email address.";
            } else {
                emailFeedback.style.color = "green";
                emailFeedback.innerText = "Valid email address.";
            }
        });
    }

    // Password strength validation
    var password = document.getElementById("password");
    var patterns = {
        minLength: /^(?=.{8,})/,
        uppercase: /[A-Z]/,
        lowercase: /[a-z]/,
        number: /\d/,
        specialChar: /[!"#$%&'()*+,-.:;<=>?@\[\\\]^_`{|}~]/
    };

    if (password) {
        var feedback = document.createElement("div");
        feedback.id = "password-strength-feedback";
        password.parentElement.appendChild(feedback);

        password.addEventListener('input', function () {
            var passwordValue = password.value;
            var valid = true;
            var message = [];

            if (!patterns.minLength.test(passwordValue)) {
                valid = false;
                message.push("Password must be at least 8 characters long.");
            }
            if (!patterns.uppercase.test(passwordValue)) {
                valid = false;
                message.push("Password must contain at least one uppercase letter.");
            }
            if (!patterns.lowercase.test(passwordValue)) {
                valid = false;
                message.push("Password must contain at least one lowercase letter.");
            }
            if (!patterns.number.test(passwordValue)) {
                valid = false;
                message.push("Password must contain at least one number.");
            }
            if (!patterns.specialChar.test(passwordValue)) {
                valid = false;
                message.push("Password must contain at least one special character.");
            }

            if (valid) {
                feedback.innerHTML = "<span class='text-success'>Password is strong.</span>";
                feedback.style.color = "green";
            } else {
                feedback.innerHTML = "<span class='text-danger'>" + message.join("<br>") + "</span>";
                feedback.style.color = "red";
            }
        });
    }

    // Phone number validation (simple example for general format)
    var phoneInput = document.getElementById("MobileNo");
    if (phoneInput) {
        phoneInput.addEventListener('input', function () {
            var phoneValue = phoneInput.value;
            var phonePattern = /^[0-9]{10}$/; // Example: Ensure 10 digits
            var phoneFeedback = document.getElementById("phone-feedback");
            if (!phonePattern.test(phoneValue)) {
                phoneFeedback.style.color = "red";
                phoneFeedback.innerText = "Please enter a valid 10-digit phone number.";
            } else {
                phoneFeedback.style.color = "green";
                phoneFeedback.innerText = "Valid phone number.";
            }
        });
    }

    // Get the textarea and character count elements
    var descriptionTextarea = document.getElementById("Description");
    var charCountDisplay = document.getElementById("charCount");

    if (descriptionTextarea && charCountDisplay) {
        var maxLength = 500; // Set maximum characters allowed

        // Update character count initially and on input
        descriptionTextarea.addEventListener("input", function () {
            var currentLength = descriptionTextarea.value.length;
            charCountDisplay.textContent = currentLength + "/" + maxLength + " characters";
        });

        // Initialize character count on page load
        var currentLength = descriptionTextarea.value.length;
        charCountDisplay.textContent = currentLength + "/" + maxLength + " characters";
    }

    //Datatable js
    var js = jQuery.noConflict(true);

    js(document).ready(function () {
        //js('#UserList').DataTable({
        //    paging: true,        // Enables pagination
        //    searching: true,     // Enables search
        //    ordering: true,      // Enables sorting
        //    info: true           // Enables table information (e.g., "Showing 1 to 10 of 100 entries")
        //});
        js('#UserList').DataTable();
    });

})(jQuery);


