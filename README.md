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


### Html Interface

The HTML Interface has basic UI to accommodate the basic operations of the API. The UI currently has the following

1. Allows you to first authenticate yourself by providing an email address. There is no restriction on the email address. It can be even a random string. No validation is applied there.
2. From there all requests are authenticated using the token generated.
3. Products are listed in a table with action columns where, you can either delete or edit a given product.
4. A simple form is provided to add new product. Create button will create a new product.
5. Clicking delete button will delete the product
6. Clicking on edit button will populate the form with selected product and the button will change to "Update". Clicking on that button will update the product details.



## Controller Details

The following are the action methods available in the **ProductController**

1. Authenticate
2. Get
3. Get (id)
4. Post (Product)
5. Put (Product)
6. Delete (id)
7. SearchProductByDescription(searchText)
8. SearchProductByBrand(searchText)
9. SearchProductByModel(searchText)