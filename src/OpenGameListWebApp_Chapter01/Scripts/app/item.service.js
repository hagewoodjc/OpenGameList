System.register(["@angular/core", "@angular/http", "rxjs/Observable", "./auth.http"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, http_1, Observable_1, auth_http_1, ItemService;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (Observable_1_1) {
                Observable_1 = Observable_1_1;
            },
            function (auth_http_1_1) {
                auth_http_1 = auth_http_1_1;
            }
        ],
        execute: function () {
            ItemService = (function () {
                function ItemService(http) {
                    this.http = http;
                    this.baseUrl = "api/items/"; //web api url
                }
                // calls the [GET] /api/items/GetLatest{n} Web API method to retrieve the latest items.
                ItemService.prototype.getLatest = function (num) {
                    var url = this.baseUrl + "GetLatest/";
                    if (num != null) {
                        url += num;
                    }
                    return this.http.get(url)
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                // calls the [GET] /api/items/GetMostViewed/{n} Web API method to retrieve the most viewed items.
                ItemService.prototype.getMostViewed = function (num) {
                    var url = this.baseUrl + "GetMostViewed/";
                    if (num != null) {
                        url += num;
                    }
                    return this.http.get(url)
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                // calls the [GET] /api/items/GetRandom/{n} Web API method to retrieve n random items.
                ItemService.prototype.getRandom = function (num) {
                    var url = this.baseUrl + "GetRandom/";
                    if (num != null) {
                        url += num;
                    }
                    return this.http.get(url)
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                // calls the [GET] /api/items/{id} Web API method to retrieve the item with the given id.
                ItemService.prototype.get = function (id) {
                    if (id == null) {
                        throw new Error("id is required.");
                    }
                    var url = this.baseUrl + id;
                    return this.http.get(url)
                        .map(function (res) { return res.json(); })
                        .catch(this.handleError);
                };
                // calls the [POST] /api/items/ Web API method to add a new item .
                ItemService.prototype.add = function (item) {
                    var url = this.baseUrl;
                    return this.http.post(url, JSON.stringify(item), this.getRequestOptions())
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                // calls the [PUT] /api/items/{id} Web API method to update an existing item.
                ItemService.prototype.update = function (item) {
                    var url = this.baseUrl + item.Id;
                    return this.http.put(url, JSON.stringify(item), this.getRequestOptions())
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                // calls [DELETE] /api/items/{id}| We b API method to delete the item with the given Id.
                ItemService.prototype.delete = function (id) {
                    var url = this.baseUrl + id;
                    return this.http.delete(url, new http_1.RequestOptions())
                        .catch(this.handleError);
                };
                // returns a viable RequestOptions object to handle Json requests
                ItemService.prototype.getRequestOptions = function () {
                    return new http_1.RequestOptions({
                        headers: new http_1.Headers({
                            "Content-Type": "application/json"
                        })
                    });
                };
                ItemService.prototype.handleError = function (error) {
                    // output errors to the console.
                    console.error(error);
                    return Observable_1.Observable.throw(error.json().error || "Server error");
                };
                return ItemService;
            }());
            ItemService = __decorate([
                core_1.Injectable(),
                __metadata("design:paramtypes", [auth_http_1.AuthHttp])
            ], ItemService);
            exports_1("ItemService", ItemService);
        }
    };
});
