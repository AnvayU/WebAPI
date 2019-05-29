

app.service('productService', function ($http) {
    //create New Record

    this.post = function (product) {
        var request = $http({
            method: "post",
            url: "/api/productAPI",
            data : product

        });
        return request;
        }
    
    //Get all products
    this.getProducts = function () {
        return $http.get("/api/ProductAPI")

    }

    //Update the Record
    this.put = function (productId, product) {
        var request = $http({
            method: "put",
            url: "/api/productAPI/" + productId,
            data: product
        });
        return request;
    }
    //Delete the Record
    this.delete = function (productId) {
        var request = $http({
            method: "delete",
            url: "/api/productAPI/" + productId
        });
        return request;
    }
})


