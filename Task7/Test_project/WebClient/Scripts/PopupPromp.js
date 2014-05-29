var opener = document.getElementById("opener");

var contolledButtons = document.getElementsByClassName("opener");


Array.prototype.forEach.call(contolledButtons, function (button) {
    // Do stuff with the element
    button.onclick = function () {
        var clonedNode = button.cloneNode(true);
        var captcha = document.getElementById("captcha"),
            dimmer = document.createElement("div");

        dimmer.style.width = window.innerWidth + 'px';
        dimmer.style.height = window.innerHeight + 'px';
        dimmer.className = 'dimmer';

        dimmer.onclick = function () {
            document.body.removeChild(this);
            captcha.removeChild(clonedNode);
            captcha.style.visibility = 'hidden';
        }

        document.body.appendChild(dimmer);
        captcha.appendChild(clonedNode);

        captcha.style.visibility = 'visible';
        captcha.style.top = window.innerHeight / 2 - 50 + 'px';
        captcha.style.left = window.innerWidth / 2 - 100 + 'px';


        return false;
    }
});
