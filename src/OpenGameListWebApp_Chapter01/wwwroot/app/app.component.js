System.register(["@angular/core","@angular/router","./auth.service"],function(exports_1,context_1){"use strict";var core_1,router_1,auth_service_1,AppComponent,__decorate=(context_1&&context_1.id,this&&this.__decorate||function(decorators,target,key,desc){var d,c=arguments.length,r=c<3?target:null===desc?desc=Object.getOwnPropertyDescriptor(target,key):desc;if("object"==typeof Reflect&&"function"==typeof Reflect.decorate)r=Reflect.decorate(decorators,target,key,desc);else for(var i=decorators.length-1;i>=0;i--)(d=decorators[i])&&(r=(c<3?d(r):c>3?d(target,key,r):d(target,key))||r);return c>3&&r&&Object.defineProperty(target,key,r),r}),__metadata=this&&this.__metadata||function(k,v){if("object"==typeof Reflect&&"function"==typeof Reflect.metadata)return Reflect.metadata(k,v)};return{setters:[function(core_1_1){core_1=core_1_1},function(router_1_1){router_1=router_1_1},function(auth_service_1_1){auth_service_1=auth_service_1_1}],execute:function(){AppComponent=function(){function AppComponent(router,authService){this.router=router,this.authService=authService,this.title="OpenGameList"}return AppComponent.prototype.isActive=function(data){return this.router.isActive(this.router.createUrlTree(data),!0)},AppComponent.prototype.logout=function(){return this.authService.logout()&&this.router.navigate([""]),!1},AppComponent=__decorate([core_1.Component({selector:"opengamelist",template:'\n        <nav class="navbar navbar-default navbar-fixed-top">\n            <div class="container-fluid">\n                <input type="checkbox" id="navbar-toggle-cbox">\n                <div class="navbar-header">\n                    <label for="navbar-toggle-cbox" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">\n                        <span class="sr-only">Toggle navigation</span>\n                        <span class="icon-bar"></span>\n                        <span class="icon-bar"></span>\n                        <span class="icon-bar"></span>\n                    </label>\n                    <a class="navbar-brand" href="javascript:void(0)">\n                        <img alt="logo" src="/img/logo.svg" />\n                    </a>\n                </div>\n                <div class="collapse navbar-collapse" id="navbar">\n                    <ul class="nav navbar-nav">\n                        <li [class.active]="isActive([\'\'])">\n                            <a class="home" [routerLink]="[\'\']">Home</a>\n                        </li>\n                        <li [class.active]="isActive([\'about\'])">\n                            <a class="about" [routerLink]="[\'about\']">About</a>\n                        </li>\n                        <li *ngIf="!authService.isLoggedIn()" [class.active]="isActive([\'login\'])">\n                            <a class="login" [routerLink]="[\'login\']">Login</a>\n                        </li>\n                        <li *ngIf="authService.isLoggedIn()">\n                            <a class="logout" href="javascript:void(0)" (click)="logout()">Logout</a>\n                        </li>\n                        <li *ngIf="authService.isLoggedIn()" [class.active]="isActive([\'item/edit\', 0])">\n                            <a class="add" [routerLink]="[\'item/edit\', 0]">Add New</a>\n                        </li>\n                    </ul>\n                </div>\n            </div>\n        </nav>\n        <h1 class="header">{{title}}</h1>\n        <div class="main-container">\n            <router-outlet></router-outlet>\n        </div>\n    '}),__metadata("design:paramtypes",[router_1.Router,auth_service_1.AuthService])],AppComponent)}(),exports_1("AppComponent",AppComponent)}}});
//# sourceMappingURL=app.component.js.map