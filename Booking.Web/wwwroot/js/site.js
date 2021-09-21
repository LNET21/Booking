
let target = document.querySelector('#createajax');

function removeForm() {
    $('#createajax').remove();
}

function fail() {
    console.log('fail');
};

$('#fetch').click(function () {
    $.ajax({
        url: "https://localhost:44339/gymclasses/fetch",
        type: 'GET',
        success: success,
        failure: fail
    });
});

function success(response) {
        target.innerHTML = response;
}