import { Component, OnInit } from '@angular/core';
import { PersonService } from 'src/app/services/person.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-my-sample',
  templateUrl: './my-sample.component.html',
  styleUrls: ['./my-sample.component.css']
})
export class MySampleComponent implements OnInit {
  myName:string;
  sampleCreateForm: FormGroup;

  yourName: string;
  yourNameResponse: string;
  constructor(
    private personService: PersonService
  ) { 
    this.sampleCreateForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      age: new FormControl('', [Validators.required]),
    })
  }

  ngOnInit() {
    this.getMyName();
  }

  async getMyName(){
try {
  let myName = await this.personService.getMyName().toPromise();
  this.myName = myName.message
  console.log(this.myName);
  console.log(myName)
} catch (error) {
  alert('An error occurred')
  console.log(error)
}
  }

  async submitYourName(){
    try {
      let response = await this.personService.getYourName(this.yourName).toPromise()

      if(response.isSuccess){
        this.yourNameResponse = response.message;
      }
    } catch (error) {
      console.log(error)
      
    }
  }
}
