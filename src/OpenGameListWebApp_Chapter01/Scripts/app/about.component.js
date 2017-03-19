System.register(["@angular/core"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, AboutComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            }
        ],
        execute: function () {
            AboutComponent = (function () {
                function AboutComponent() {
                    this.title = "About";
                }
                return AboutComponent;
            }());
            AboutComponent = __decorate([
                core_1.Component({
                    selector: "about",
                    template: "\n        <h2>{{title}}</h2>\n        <div>\n            OpenGameList: a production-ready, fully-featured SPA sample powered by ASP.NET Core Web API and Angular2.\n        </div>\n    "
                })
            ], AboutComponent);
            exports_1("AboutComponent", AboutComponent);
        }
    };
});
