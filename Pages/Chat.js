// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
    apiKey: "AIzaSyB2Clflt1ZRRgCYF_xuVn83GDpv4VY7Dmw",
    authDomain: "chatscreen-e27fb.firebaseapp.com",
    projectId: "chatscreen-e27fb",
    storageBucket: "chatscreen-e27fb.appspot.com",
    messagingSenderId: "248623177383",
    appId: "1:248623177383:web:f1e8a14e8666b33bbf0582",
    measurementId: "G-TT7ZCJN2WD"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
const db = firebase.database();

document.getElementById("message").addEventListener("submit", sendMessage);

function sendMessage(e) {
    e.preventDefault();

    const timestamp = Date.now();
    const messageInput = document.getElementById("message-send");
    const message = messageInput.value;

    messageInput.value = "";

    document
        .getElementById("message")
        .scrollIntoView({ behavior: "smooth", block: "end", inline: "nearest" });

    db.ref("message/" + timestamp).set({
        username,
        message,
    });
}

const getChat = db.ref("message/");

getChat.on("child_added", function (snapshot) {
    const messages = snapshot.val();
    const message = <li class=${
        username == messages.username ? "sent" : "receive"
    }><span>$(messages.username}: </span>${messages.message}</li>

    document.getElementById("messages").innerHTML += message;

})