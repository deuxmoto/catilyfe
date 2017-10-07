import { Injectable } from "@angular/core";

import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";

@Injectable()
export class AdminService {

    private refreshAdminViewEmitter = new Subject<void>();

    public emitRefreshAdminView(): void {
        this.refreshAdminViewEmitter.next();
    }

    public observableRefreshAdminViewEmitter(): Observable<void> {
        return this.refreshAdminViewEmitter.asObservable();
    }
}
