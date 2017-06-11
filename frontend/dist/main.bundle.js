webpackJsonp([1,4],{

/***/ 154:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(15)(false);
// imports


// module
exports.push([module.i, ".main {\n  height: 100%; }\n\n.footer {\n  background-color: #313131;\n  padding: 3rem 3rem 4rem; }\n  .footer > * {\n    color: #e6e6e6; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 155:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(15)(false);
// imports


// module
exports.push([module.i, "/*\n * Hero\n */\n.home-hero {\n  height: 100%;\n  display: -webkit-box;\n  display: -ms-flexbox;\n  display: flex;\n  -webkit-box-align: center;\n      -ms-flex-align: center;\n          align-items: center;\n  -webkit-box-pack: center;\n      -ms-flex-pack: center;\n          justify-content: center;\n  max-width: 100%;\n  margin-bottom: 40px;\n  background-image: url(" + __webpack_require__(191) + ");\n  background-repeat: no-repeat;\n  background-position: center;\n  background-size: cover; }\n\n.title-container {\n  background-color: rgba(0, 0, 0, 0.6);\n  padding: 2em;\n  text-align: right;\n  border: 1px solid rgba(255, 255, 255, 0.8); }\n  .title-container > * {\n    color: #fafafa;\n    text-shadow: 0px 1px rgba(0, 0, 0, 0.8); }\n\n.hero-title {\n  position: relative;\n  font-size: 3em;\n  margin-bottom: 0;\n  font-family: 'Lobster', cursive; }\n\n.hero-subtitle {\n  margin-top: -0.5em;\n  font-family: 'Cormorant Infant', serif; }\n\n@media (min-width: 960px) {\n  .title-container {\n    padding: 4em; }\n  .hero-title {\n    font-size: 5.5em; }\n  .hero-subtitle {\n    font-size: 1.5em; } }\n\n/*\n * Post preview\n */\n.recent-posts {\n  display: -webkit-box;\n  display: -ms-flexbox;\n  display: flex;\n  -webkit-box-align: center;\n      -ms-flex-align: center;\n          align-items: center;\n  -webkit-box-orient: vertical;\n  -webkit-box-direction: normal;\n      -ms-flex-direction: column;\n          flex-direction: column;\n  width: 100%; }\n\n.post-preview {\n  width: 80%;\n  padding: 30px;\n  margin-bottom: 30px;\n  cursor: pointer;\n  border: 1px solid rgba(0, 0, 0, 0.1);\n  box-shadow: 0 2px 2px rgba(0, 0, 0, 0.4);\n  transition: box-shadow 150ms; }\n  .post-preview:hover, .post-preview:active {\n    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.6); }\n\n.post-preview-header {\n  padding-bottom: 10px;\n  margin-bottom: 15px;\n  border-bottom: 1px solid rgba(0, 0, 0, 0.2); }\n\n.post-preview-title {\n  font-family: 'Lobster', cursive;\n  font-size: 2rem; }\n\n.post-preview-subtitle {\n  text-transform: uppercase;\n  font-weight: bold;\n  font-size: 0.8rem; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 156:
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__(15)(false);
// imports


// module
exports.push([module.i, ".post-section {\n  min-height: 100%;\n  width: 100%;\n  padding: 20px 0;\n  display: -webkit-box;\n  display: -ms-flexbox;\n  display: flex;\n  -webkit-box-pack: center;\n      -ms-flex-pack: center;\n          justify-content: center; }\n\n.post-container {\n  margin: 0 20px;\n  min-width: 100%; }\n  @media (min-width: 960px) {\n    .post-container {\n      min-width: 960px;\n      max-width: 960px; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ 157:
/***/ (function(module, exports) {

module.exports = "<div class=\"main\">\n    <router-outlet></router-outlet>\n    <footer class=\"footer\">\n        <p class=\"is-pulled-left\">© 2017 - Justin Niles and Peter Sulucz</p>\n        <p class=\"is-pulled-right\">Made in <a href=\"http://www.estately.com/blog/2013/10/bellevue-vs-seattle-which-is-the-better-place-to-live/\">Seattle and Bellevue</a>, WA</p>\n    </footer>\n</div>\n"

/***/ }),

/***/ 158:
/***/ (function(module, exports) {

module.exports = "<div class=\"home-hero\">\n    <div class=\"title-container\">\n        <h1 class=\"hero-title\">\n            #Cati Lyfe\n        </h1>\n        <h3 class=\"hero-subtitle\">A lyfestyle blog by Justin and Peter</h3>\n    </div>\n</div>\n\n<ul class=\"recent-posts\">\n    <li class=\"post-preview\" *ngFor=\"let post of recentPosts | async\" (click)=\"navigateToPost(post.slug)\">\n        <div class=\"post-preview-header\">\n            <h2 class=\"post-preview-title\">{{post.title}}</h2>\n            <h3 class=\"post-preview-subtitle\">{{post.whenPublished | date:\"MMMM d, y - h:mma\"}}</h3>\n        </div>\n        <div class=\"post-preview-body\">\n            <p class=\"post-preview-description\">{{post.description}}</p>\n        </div>\n    </li>\n</ul>\n"

/***/ }),

/***/ 159:
/***/ (function(module, exports) {

module.exports = "<div class=\"post-section\">\n    <div class=\"post-container content\" [innerHtml]=\"postHtml\">\n    </div>\n</div>\n"

/***/ }),

/***/ 191:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__.p + "catilyfe-hero-image.31624ca9750258d85f47.jpg";

/***/ }),

/***/ 193:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(88);


/***/ }),

/***/ 34:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__(61);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_Observable__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_observable_throw__ = __webpack_require__(164);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_observable_throw___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_rxjs_add_observable_throw__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_catch__ = __webpack_require__(74);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_catch___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_catch__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_rxjs_add_operator_map__ = __webpack_require__(51);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_rxjs_add_operator_map___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_5_rxjs_add_operator_map__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return PostNotFoundError; });
/* unused harmony export UnknownError */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BackendApiService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var PostNotFoundError = (function () {
    function PostNotFoundError() {
    }
    return PostNotFoundError;
}());

var UnknownError = (function () {
    function UnknownError() {
    }
    return UnknownError;
}());

var BackendEndpoint = "http://caticake.azurewebsites.net";
var BackendApiService = (function () {
    function BackendApiService(http) {
        this.http = http;
    }
    BackendApiService.prototype.getRecentPostMetadata = function (count) {
        var _this = this;
        return this.http.get(BackendEndpoint + "/postmetadata?$top=" + count)
            .map(function (response) {
            var responseArray = response.json();
            return responseArray.map(function (postMetadata) { return _this.parseServerPostMetadata(postMetadata); });
        });
    };
    BackendApiService.prototype.getPost = function (slug) {
        var _this = this;
        return this.http.get(BackendEndpoint + "/post/" + slug)
            .map(function (response) {
            var post = response.json();
            post.metadata = _this.parseServerPostMetadata(post.metadata);
            return post;
        })
            .catch(function (error) {
            if (error instanceof __WEBPACK_IMPORTED_MODULE_1__angular_http__["b" /* Response */] && error.status === 404) {
                return __WEBPACK_IMPORTED_MODULE_2_rxjs_Observable__["Observable"].throw(new PostNotFoundError());
            }
            else {
                console.error(error);
                return __WEBPACK_IMPORTED_MODULE_2_rxjs_Observable__["Observable"].throw(new UnknownError());
            }
        });
    };
    /**
     * Ensures that the post metadata is in the format expected by the client
     * (i.e. Dates are proper JS Date objects and not strings, etc.)
     */
    BackendApiService.prototype.parseServerPostMetadata = function (postMetadata) {
        postMetadata.whenPublished = new Date(postMetadata.whenPublished);
        return postMetadata;
    };
    return BackendApiService;
}());
BackendApiService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["c" /* Injectable */])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_http__["c" /* Http */]) === "function" && _a || Object])
], BackendApiService);

var _a;
//# sourceMappingURL=backend-api.service.js.map

/***/ }),

/***/ 87:
/***/ (function(module, exports) {

function webpackEmptyContext(req) {
	throw new Error("Cannot find module '" + req + "'.");
}
webpackEmptyContext.keys = function() { return []; };
webpackEmptyContext.resolve = webpackEmptyContext;
module.exports = webpackEmptyContext;
webpackEmptyContext.id = 87;


/***/ }),

/***/ 88:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__ = __webpack_require__(93);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_app_module__ = __webpack_require__(95);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__environments_environment__ = __webpack_require__(99);




if (__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].production) {
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["a" /* enableProdMode */])();
}
__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_2__app_app_module__["a" /* AppModule */]);
//# sourceMappingURL=main.js.map

/***/ }),

/***/ 94:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(3);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var AppComponent = (function () {
    function AppComponent() {
        this.title = "app works!";
    }
    return AppComponent;
}());
AppComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_14" /* Component */])({
        selector: "app-root",
        template: __webpack_require__(157),
        styles: [__webpack_require__(154)]
    })
], AppComponent);

//# sourceMappingURL=app.component.js.map

/***/ }),

/***/ 95:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__ = __webpack_require__(16);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_forms__ = __webpack_require__(92);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_http__ = __webpack_require__(61);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_router__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__app_component__ = __webpack_require__(94);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__core_core_module__ = __webpack_require__(96);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__home_home_component__ = __webpack_require__(97);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__post_post_component__ = __webpack_require__(98);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};









var routes = [
    {
        path: "",
        component: __WEBPACK_IMPORTED_MODULE_7__home_home_component__["a" /* HomeComponent */]
    },
    {
        path: "posts/:slug",
        component: __WEBPACK_IMPORTED_MODULE_8__post_post_component__["a" /* PostComponent */]
    }
];
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["b" /* NgModule */])({
        declarations: [
            __WEBPACK_IMPORTED_MODULE_5__app_component__["a" /* AppComponent */],
            __WEBPACK_IMPORTED_MODULE_7__home_home_component__["a" /* HomeComponent */],
            __WEBPACK_IMPORTED_MODULE_8__post_post_component__["a" /* PostComponent */]
        ],
        imports: [
            __WEBPACK_IMPORTED_MODULE_4__angular_router__["a" /* RouterModule */].forRoot(routes),
            __WEBPACK_IMPORTED_MODULE_6__core_core_module__["a" /* CoreModule */].forRoot(),
            __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__["a" /* BrowserModule */],
            __WEBPACK_IMPORTED_MODULE_2__angular_forms__["a" /* FormsModule */],
            __WEBPACK_IMPORTED_MODULE_3__angular_http__["a" /* HttpModule */]
        ],
        providers: [],
        bootstrap: [__WEBPACK_IMPORTED_MODULE_5__app_component__["a" /* AppComponent */]]
    })
], AppModule);

//# sourceMappingURL=app.module.js.map

/***/ }),

/***/ 96:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__(23);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__backend_api_service__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CoreModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};



var CoreModule = CoreModule_1 = (function () {
    function CoreModule(parentModule) {
        if (parentModule) {
            throw new Error("CoreModule is already loaded. Import it in the AppModule only");
        }
    }
    CoreModule.forRoot = function () {
        return {
            ngModule: CoreModule_1,
            providers: [
                __WEBPACK_IMPORTED_MODULE_2__backend_api_service__["a" /* BackendApiService */]
            ]
        };
    };
    return CoreModule;
}());
CoreModule = CoreModule_1 = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_common__["c" /* CommonModule */]],
        providers: [__WEBPACK_IMPORTED_MODULE_2__backend_api_service__["a" /* BackendApiService */]]
    }),
    __param(0, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["o" /* Optional */])()), __param(0, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["E" /* SkipSelf */])()),
    __metadata("design:paramtypes", [CoreModule])
], CoreModule);

var CoreModule_1;
//# sourceMappingURL=core.module.js.map

/***/ }),

/***/ 97:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_operator_map__ = __webpack_require__(51);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_operator_map___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_rxjs_add_operator_map__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_backend_api_service__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HomeComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var HomeComponent = (function () {
    function HomeComponent(backend, router) {
        this.backend = backend;
        this.router = router;
    }
    HomeComponent.prototype.ngOnInit = function () {
        this.recentPosts = this.backend.getRecentPostMetadata(10);
    };
    HomeComponent.prototype.navigateToPost = function (postSlug) {
        this.router.navigate(["posts", postSlug]);
    };
    return HomeComponent;
}());
HomeComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_14" /* Component */])({
        selector: "app-home",
        template: __webpack_require__(158),
        styles: [__webpack_require__(155)]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__core_backend_api_service__["a" /* BackendApiService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__core_backend_api_service__["a" /* BackendApiService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__angular_router__["c" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_router__["c" /* Router */]) === "function" && _b || Object])
], HomeComponent);

var _a, _b;
//# sourceMappingURL=home.component.js.map

/***/ }),

/***/ 98:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_operator_map__ = __webpack_require__(51);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_operator_map___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_rxjs_add_operator_map__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_catch__ = __webpack_require__(74);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_catch___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_catch__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_backend_api_service__ = __webpack_require__(34);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PostComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var PostComponent = (function () {
    function PostComponent(backend, route, router) {
        this.backend = backend;
        this.route = route;
        this.router = router;
        this.validPost = true;
    }
    PostComponent.prototype.ngOnInit = function () {
        var _this = this;
        var slug = this.route.snapshot.paramMap.get("slug");
        this.backend.getPost(slug).subscribe(function (post) {
            _this.postHtml = post.rawHtmlThenIGuess;
            _this.validPost = true;
        }, function (error) {
            if (error instanceof __WEBPACK_IMPORTED_MODULE_4__core_backend_api_service__["b" /* PostNotFoundError */]) {
                _this.validPost = false;
                console.error("invalid post");
            }
            return null;
        });
    };
    return PostComponent;
}());
PostComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_14" /* Component */])({
        selector: "app-post",
        template: __webpack_require__(159),
        styles: [__webpack_require__(156)]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_4__core_backend_api_service__["a" /* BackendApiService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__core_backend_api_service__["a" /* BackendApiService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* ActivatedRoute */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* ActivatedRoute */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_1__angular_router__["c" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_router__["c" /* Router */]) === "function" && _c || Object])
], PostComponent);

var _a, _b, _c;
//# sourceMappingURL=post.component.js.map

/***/ }),

/***/ 99:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
// The file contents for the current environment will overwrite these during build.
var environment = {
    production: false
};
//# sourceMappingURL=environment.js.map

/***/ })

},[193]);
//# sourceMappingURL=main.bundle.js.map