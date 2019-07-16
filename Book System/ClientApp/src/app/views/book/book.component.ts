import { Component, OnInit } from '@angular/core';
import { BookService } from 'src/app/services/book.service';
import { BookDataService } from 'src/app/dataservices/book.dataService';
import { Book } from 'src/app/models/book.models';
import { BookUpdateFormComponent } from './book-update-form/book-update-form.component';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
  books: Book[];

  constructor(
    private bookService: BookService,
    private bookDataService: BookDataService,
    
  ) { }

  ngOnInit() {
    this.bookDataService.books.subscribe(data => {
      this.getBooks();
    })
  }

  async getBooks(){
    try {
      this.books = await this.bookService.getAll().toPromise();
    } catch (error) {
      alert('Something went wrong!');
      console.error(error);
    }
  }

  async delete(id){
    if(confirm('Are you sure yo uwant to delete?')){
      try {
        let result = await this.bookService.delete(id).toPromise()
        if(result.isSuccess){
          alert(result.message)
          this.bookDataService.refreshBooks();

        }else{
          alert(result.message)
        }
      } catch (error) {
        alert('Something went wrong');
        console.log(error)
      }
    }
  }

  update(book){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      bookContext: book
    };
    dialogConfig.width = '600px';
    this.dialog.open(BookUpdateFormComponent, dialogConfig)
  }

}
