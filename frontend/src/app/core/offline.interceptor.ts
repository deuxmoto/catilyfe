import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpResponse,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/add/observable/of";

import { BackendHostName } from "./backend-api.service";
import { environment } from "../../environments/environment";

const relativeUrlRegex = new RegExp(`^.*${BackendHostName.replace(/\./g, "\\.")}\/((?:[^\?#])*)`);
const offlineRoutes = environment.offlineRoutes;
const interceptingRequests = !!offlineRoutes;

@Injectable()
export class OfflineInterceptor implements HttpInterceptor {
    constructor() {
        if (interceptingRequests) {
            console.warn("Running catilyfe in offline mode.");
        }
    }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const requestUrl = request.url;
        if (!interceptingRequests) {
            return next.handle(request);
        }

        const parsedUrl = request.url.match(relativeUrlRegex);
        const relativeUrl = parsedUrl.length > 1 ? parsedUrl[1] : "";

        // Remove all non-word characters
        const convertedUrlName = relativeUrl.replace(/\W/g, "");

        const responseBody = offlineRoutes[convertedUrlName];
        if (responseBody) {
            console.info(`Intercepted route ${convertedUrlName}`);
            const response = new HttpResponse({
                body: responseBody
            });
            return Observable.of(response);
        }

        throw new Error(
            `Could not find an offline route for '${convertedUrlName}'. ` +
            `Configured offline routes are: ${Object.keys(offlineRoutes).join(",")}`
        );
    }
}
