const apiURL = 'api/products';
let products = [];

function listProducts() {
    getToken();
    fetch(apiURL, {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer ' + getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then(data => renderData(data))
        .catch(error => console.error('Unable to load products.', error));
}

function validateForm() {
    const product = getProductDataModel();
    let isValidRequest = true;

    if (product.description === "") {
        isValidRequest = false;
    }

    if (product.model === "") {
        isValidRequest = false;
    }

    if (product.brand === "") {
        isValidRequest = false;
    }
    return isValidRequest;
}

function getToken() {
    let hiddenToken = document.getElementById('hiddenToken');
    return hiddenToken.value;
}

function authenticate() {
    const email = document.getElementById('loginEmail');

    fetch(apiURL + '/authenticate', {
        method: 'POST',
        body: JSON.stringify({ EmailAddress: email.value }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then(data => storeToken(data))
        .catch(error => console.error('Unable to authenticate.', error));
}

function updateProduct() {
    const requestMethod = 'PUT';
    if (validateForm()) {
        addOrUpdateProduct(requestMethod);
    } else {
        alert('Please provide form input');
    }

    document.getElementById('addProduct').style.display = "block";
    document.getElementById('updateProduct').style.display = "none";
}

function getProductDataModel() {
    const productId = document.getElementById('productId');
    const productDescription = document.getElementById('productDescription');
    const productModel = document.getElementById('productModel');
    const productBrand = document.getElementById('productBrand');
    const product = {
        id: productId.value,
        description: productDescription.value,
        model: productModel.value,
        brand: productBrand.value,
    };
    return product;
}

function addProduct() {
    if (validateForm()) {
        const requestMethod = 'POST';
        addOrUpdateProduct(requestMethod);
    } else {
        alert('Please provide form input');
    }
}

function addOrUpdateProduct(requestMethod) {
    const product = getProductDataModel();

    fetch(apiURL, {
        headers: {
            'Authorization': 'Bearer ' + getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: requestMethod,
        body: JSON.stringify(product)
    }).then(function (response) {
        if (response.status !== 200) {
            alert('Error Adding Product');
            console.log('Status Code Error: ' + response.status);
            return;
        } else {
            listProducts();
            clearInputControls();
        }
    });
}

function clearInputControls() {
    document.getElementById('productId').value = '';
    document.getElementById('productDescription').value = '';
    document.getElementById('productModel').value = '';
    document.getElementById('productBrand').value = '';
}

function storeToken(data) {
    const hiddenToken = document.getElementById('hiddenToken');
    hiddenToken.value = data.token;
    showHideSections();
    listProducts();
}

function renderData(data) {
    const tableBody = document.getElementById("productsTableBody");
    tableBody.innerHTML = '';
    const button = document.createElement('button');
    data.forEach(product => {
        let labelDescription = document.createElement('label');
        labelDescription.type = 'label';
        labelDescription.innerText = product.description;

        let labelModel = document.createElement('label');
        labelModel.type = 'label';
        labelModel.innerText = product.model;

        let labelBrand = document.createElement('label');
        labelBrand.type = 'label';
        labelBrand.innerText = product.brand;

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteProduct(${product.id})`);
        deleteButton.setAttribute('class', 'btn-default');

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `editProduct(${product.id})`);
        deleteButton.setAttribute('class', 'btn-primary');

        let tableRow = tableBody.insertRow();

        let tableData1 = tableRow.insertCell(0);
        tableData1.appendChild(labelBrand);

        let tableData2 = tableRow.insertCell(1);
        tableData2.appendChild(labelModel);

        let tableData3 = tableRow.insertCell(2);
        tableData3.appendChild(labelDescription);

        let tableData4 = tableRow.insertCell(3);
        tableData4.appendChild(editButton);
        tableData4.appendChild(deleteButton);
    });
}

function showHideSections() {
    var hiddenToken = document.getElementById("hiddenToken");
    var unauthenticatedDiv = document.getElementById("unauthenticatedDiv");
    var authenticatedDiv = document.getElementById("authenticatedDiv");

    if (hiddenToken.value === '') {
        authenticatedDiv.style.display = "none";
        unauthenticatedDiv.style.display = "block";
    } else {
        authenticatedDiv.style.display = "block";
        unauthenticatedDiv.style.display = "none";
    }
}


function editProduct(id) {
    var getProductURL = apiURL + '/' + id;
    fetch(getProductURL, {
        headers: {
            'Authorization': 'Bearer ' + getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: "GET"
    })
        .then(response => response.json())
        .then(function (data) {
            loadProduct(data);
        })
        .catch(error => console.error('Unable to get products.', error));
}

function loadProduct(data) {
    document.getElementById('productId').value = data.id;
    document.getElementById('productDescription').value = data.description;
    document.getElementById('productModel').value = data.model;
    document.getElementById('productBrand').value = data.brand;
    document.getElementById('addProduct').style.display = "none";
    document.getElementById('updateProduct').style.display = "block";
}

function deleteProduct(id) {
    var deleteProductURL = apiURL + '/' + id;
    fetch(deleteProductURL, {
        headers: {
            'Authorization': 'Bearer ' + getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: "DELETE"
    }).then(function (response) {
        if (response.status !== 200) {
            alert('Error Deleting Product');
            console.log('Status Code Error: ' + response.status);
            return;
        } else {
            listProducts();
        }
    });
}
