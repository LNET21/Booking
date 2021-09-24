
let target = document.querySelector('#createajax');
document.querySelector('#fetch').addEventListener('click', callback);


function callback() {
    fetch('https://localhost:44339/gymclasses/fetch',
        {
            method: 'GET',
            headers: {

            }
        })
        .then(res => res.text())
        .then(data => {
            target.innerHTML = data;
            fixvalidation();
        })
        .catch(err => console.log(err));
}

function removeForm() {
    target.innerHTML = '';
}

function fail() {
    console.log('fail');
};

function failcreate(response) {
    console.log(response, 'fail to add gymclass');
    target.innerHTML = response.responseText;
};

$('#ajax').click(function () {
    $.ajax({
        url: "https://localhost:44339/gymclasses/ajax",
        type: 'GET',
        success: success,
        failure: fail
    });
});

function success(response) {
    target.innerHTML = response;
    fixvalidation();
}

function fixvalidation() {
    console.log('Create Form loaded');
    const form = target.querySelector('form');
    $.validator.unobtrusive.parse(form);
}

$('#checkbox').click(function() {
    $('form').submit();
});