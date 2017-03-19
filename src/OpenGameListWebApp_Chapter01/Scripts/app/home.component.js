System.register(["@angular/core"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, HomeComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            }
        ],
        execute: function () {
            HomeComponent = (function () {
                function HomeComponent() {
                    this.title = "Welcome View";
                }
                return HomeComponent;
            }());
            HomeComponent = __decorate([
                core_1.Component({
                    selector: "home",
                    template: "<h2>\n                    A non-comprehensive directory of open-source video games available on the web. \n               </h2>\n                <div class=\"col-md-4\">\n                    <item-list class=\"latest\"></item-list>\n                </div>\n                <div class=\"col-md-4\">\n                    <item-list class=\"most-viewed\"></item-list>\n                </div>\n                <div class=\"col-md-4\">\n                    <item-list class=\"random\"></item-list>\n                </div>\n            "
                })
            ], HomeComponent);
            exports_1("HomeComponent", HomeComponent);
        }
    };
});
