import { NgModule }            from '@angular/core';
import { CommonModule }        from '@angular/common';
import { FormsModule }         from '@angular/forms';

import { FooterComponent } from "./footer/footer.component";

@NgModule({
  imports:      [ CommonModule ],
  declarations: [ FooterComponent ],
  exports:      [ FooterComponent, CommonModule, FormsModule ]
})
export class SharedModule { }
