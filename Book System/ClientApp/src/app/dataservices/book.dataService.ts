import { Inject, Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Book } from '../models/book.models';

@Injectable({
    providedIn: 'root'
})

export class BookDataService implements OnDestroy {
    bookSource = new BehaviorSubject<Book[]>([]);
    books = this.bookSource.asObservable();
constructor(){

}

refreshBooks() {
    this.bookSource.next(null);
}

ngOnDestroy() {
    this.bookSource.unsubscribe();
}

}
