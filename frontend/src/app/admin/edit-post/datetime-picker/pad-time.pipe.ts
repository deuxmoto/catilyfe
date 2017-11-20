import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "padtime" })
export class PadTimePipe implements PipeTransform {
    public transform(value: number): string {
        return value < 10 ? "0" + value : "" + value;
    }
}
