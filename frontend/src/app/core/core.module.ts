import {
    ModuleWithProviders, NgModule,
    Optional, SkipSelf
} from "@angular/core";

import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";

import { BackendApiService } from "./backend-api.service";

@NgModule({
    imports: [ CommonModule, HttpClientModule ],
    providers: [ BackendApiService ]
})
export class CoreModule {

    constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error(
                "CoreModule is already loaded. Import it in the AppModule only");
        }
    }

    static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
                BackendApiService
            ]
        };
    }
}
