
// ���������� ��������� ����
var key = CryptoJS.enc.Hex.parse(CryptoJS.lib.WordArray.random(16).toString());

function encryptMessage(plaintext) {
    
    // ����������� ��������� � �������� ������
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
    //// ����������� ������������� ����� � ������ ��� �������� �� ������
    //var ciphertextString = ciphertext.toString();
    //// ����������� ���� � ������ ��� �������� �� ������
    //var keyString = CryptoJS.enc.Hex.stringify(key);
    //return ciphertextString;
// ���������� ������������� ����� � ���� �� ������
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


//// ���������� ��������� ����
//var key = CryptoJS.enc.Hex.parse(CryptoJS.lib.WordArray.random(16).toString());
//// ����������� ��������� � �������� ������
//var message = CryptoJS.enc.Utf8.parse("Hello world");
//// ������� ��������� � �������������� AES � �����
//var ciphertext = CryptoJS.AES.encrypt(message, key, {
//    mode: CryptoJS.mode.ECB,
//    padding: CryptoJS.pad.Pkcs7
//});
//// ����������� ������������� ����� � ������ ��� �������� �� ������
//var ciphertextString = ciphertext.toString();
//// ����������� ���� � ������ ��� �������� �� ������
//var keyString = CryptoJS.enc.Hex.stringify(key);
//// ���������� ������������� ����� � ���� �� ������