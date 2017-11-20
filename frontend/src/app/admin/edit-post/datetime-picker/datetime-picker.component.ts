import { Component, Input, OnInit, forwardRef } from "@angular/core";
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from "@angular/forms";

const createArrayWithElementsFrom0To = (lastValue: number): number[] => {
    return Array(++lastValue).fill(1).map((v, i) => i);
};

@Component({
    selector: "datetime-picker",
    templateUrl: "./datetime-picker.component.html",
    styleUrls: ["./datetime-picker.component.scss"],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => DateTimePickerComponent),
            multi: true
        }
    ]
})
export class DateTimePickerComponent implements OnInit, ControlValueAccessor {
    public dayMonthYear: Date;
    public hour: number;
    public minute: number;

    public availableHours = createArrayWithElementsFrom0To(23);
    public availableMinutes = createArrayWithElementsFrom0To(59);
    public isDisabled = false;

    @Input()
    public namePrefix: string;

    private _onChange: (date: Date) => void;

    public ngOnInit(): void {
        this.writeValue(new Date());
    }

    public onChange(): void {
        const updatedDate = new Date(this.dayMonthYear.getTime());
        updatedDate.setUTCHours(this.hour, this.minute);
        this._onChange(updatedDate);
    }

    public writeValue(date: Date): void {
        if (date) {
            this.dayMonthYear = new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate());
            this.hour = date.getUTCHours();
            this.minute = date.getUTCMinutes();
        }
    }

    public registerOnChange(fn): void {
        this._onChange = fn;
    }

    public registerOnTouched(): void {}

    public setDisabledState(isDisabled: boolean): void {
        this.isDisabled = isDisabled;
    }
}
