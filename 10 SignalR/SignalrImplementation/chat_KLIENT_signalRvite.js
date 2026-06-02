"use strict";
// Polaczenie z hubem na serwerze SignalR.
// Port 7099 = port HTTPS aplikacji SignalrImplementation (z launchSettings.json -> profil "https").
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7099/chatHub").build();

// Przycisk zablokowany dopoki polaczenie sie nie nawiaze.
document.getElementById("sendButton").disabled = true;

// Odbieranie wiadomosci rozglaszanych przez hub (Clients.All).
connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} says ${message}`;
});

// Start polaczenia -> dopiero teraz odblokowujemy przycisk.
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// Wyslanie wiadomosci -> wywolanie metody SendMessage(user, message) na hubie.
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
