import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BookService } from 'src/app/services/book.service';
import { BookDataService } from 'src/app/dataservices/book.dataService';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Book } from 'src/app/models/book.models';

@Component({
  selector: 'app-book-update-form',
  templateUrl: './book-update-form.component.html',
  styleUrls: ['./book-update-form.component.css']
})
export class BookUpdateFormComponent implements OnInit {
  isSubmit = false;
  titleBackEndErrors: string[];
  copyrightBackEndErrors: string[];

  bookContext: any;
  bookEditForm: FormGroup;
  bookToBeEditted: Book;

  constructor(
    private bookService: BookService,
    private bookDataService: BookDataService,
    public dialogRef: MatDialogRef<BookUpdateFormComponent>,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.bookEditForm = new FormGroup({
      id: new FormControl(),
      title: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      copyright: new FormControl('', [Validators.required, Validators.maxLength(4)]),
    })
    this.bookContext = data;
   }

   get f() {return this.bookEditForm.controls;}

  ngOnInit() {
    this.bookToBeEditted = this.bookContext.bookContext;
    this.change();
  }

  change(){
    this.bookEditForm.controls['title'].setValue(this.bookToBeEditted.title);
    this.bookEditForm.controls['copyright'].setValue(this.bookToBeEditted.copyright);
  }

  close(){
    this.dialogRef.close();
  }
  async onFormSubmit(){
    let ok = confirm("Are you sure you want to submit?");
    // ends the function if the user did not confirm
    if(!ok){
      return;
    }
      if (!this.bookEditForm.valid)
      return;

      try {
        this.isSubmit = true;
        this.titleBackEndErrors = null;
        this.copyrightBackEndErrors = null;
        this.bookEditForm.value.id = this.bookToBeEditted.id;
        let result = await this.bookService.update(this.bookEditForm.value).toPromise();
        if(result.isSuccess) {
          alert(result.message);
          this.bookEditForm.reset();
          this.bookDataService.refreshBooks();
          this.close();
          
        }
        else {
          alert(result.message);
        }
      } catch (error) {
        console.error(error)
        let errs = error.error
        
        if(errs.isSuccess === false){
          alert(errs.message);
          return;
        }
        if(errs.errors){
          if('title' in errs.errors) {
            this.titleBackEndErrors = errs.errors.title;
          }
          if(errs.errors){
            if('copyright' in errs.errors) {
              this.copyrightBackEndErrors = errs.errors.copyright;
            }
        }
      }
        this.isSubmit = false;
      } finally{
        this.isSubmit = false;
      }
  }
}
