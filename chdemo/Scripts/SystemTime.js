function startTime() {
    var today = new Date();
    var y = today.getFullYear();
    var mon = today.getMonth();
    var d = today.getDate();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    var month = mon + 1;
 
    m = checkTime(m);
    s = checkTime(s);
    document.getElementById('time').innerHTML =
        y + "/" + month + "/" + d + " " + h + ":" + m + ":" + s;
    var t = setTimeout(startTime, 500);
}
function checkTime(i) {
    if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
    return i;
}