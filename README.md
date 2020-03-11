# ProdHiFiApi

The code is a sample implementation of a **web api** that is used for Product management for a **estore**. The API is quite basic in its implementation has some basic CRUD methods. The project also has unit tests that some basic testing of the API

## Controller Methods

 1. GET - Get all products.
 2. GET(id)  - Get By Id.
 3. POST(model) - Adding a new product.
 4. PUT (model) - Updating an existing product.
 5. DELETE(id)  - Removing an existing product.

## Authentication Methodology

I am using **JWT Authorization** header using the Bearer scheme


## Implementation Details


### Swagger

The project is configured to use swagger. The first method to execute should be the authenticate which accepts a random string and returns a token. Then the Authorization token in the swagger project can be configured with this generate token. Once done, rest of the methods are executed using the token which will authorize the requests.



