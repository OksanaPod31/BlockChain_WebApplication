
// генерируем случайный ключ
var key = CryptoJS.enc.Hex.parse(CryptoJS.lib.WordArray.random(16).toString());

function encryptMessage(plaintext) {
    
    // преобразуем сообщение в байтовый массив
    //var message = CryptoJS.enc.Utf8.parse(plaintext);

    var res = encryptMessageWithKey(plaintext, key);
    return res;
}

function encryptMessageWithKey(message, key) {
    var data = CryptoJS.AES.encrypt(message, key);
    return data.toString();
    //var ciphertext = CryptoJS.AES.encrypt(message, key, {
    //    mode: CryptoJS.mode.ECB,
    //    padding: CryptoJS.pad.Pkcs7
    //});
    //// преобразуем зашифрованный текст в строку для передачи на сервер
    //var ciphertextString = ciphertext.toString();
    //// преобразуем ключ в строку для передачи на сервер
    //var keyString = CryptoJS.enc.Hex.stringify(key);
    //return ciphertextString;
// отправляем зашифрованный текст и ключ на сервер
}
function descryptMessage(ciphertext) {
    var res = descryptMessageWithKey(ciphertext, key);
    return res;
}

function descryptMessageWithKey(ciphertext, key) {
    var bytes = CryptoJS.AES.decrypt(ciphertext, key);
    var plaintext = bytes.toString(CryptoJS.enc.Utf8);
    return plaintext;
}


//// генерируем случайный ключ
//var key = CryptoJS.enc.Hex.parse(CryptoJS.lib.WordArray.random(16).toString());
//// преобразуем сообщение в байтовый массив
//var message = CryptoJS.enc.Utf8.parse("Hello world");
//// шифруем сообщение с использованием AES и ключа
//var ciphertext = CryptoJS.AES.encrypt(message, key, {
//    mode: CryptoJS.mode.ECB,
//    padding: CryptoJS.pad.Pkcs7
//});
//// преобразуем зашифрованный текст в строку для передачи на сервер
//var ciphertextString = ciphertext.toString();
//// преобразуем ключ в строку для передачи на сервер
//var keyString = CryptoJS.enc.Hex.stringify(key);
//// отправляем зашифрованный текст и ключ на сервер