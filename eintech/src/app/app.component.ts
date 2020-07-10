import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {Observable, throwError} from 'rxjs';
import {catchError, retry} from 'rxjs/operators';
import {stringify} from "@angular/compiler/src/util";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  title: string = 'Person';

  name: string;

  constructor(private http: HttpClient) {

  }

  onSubmit(): void {

    var person: IPerson = {
      name: this.name,
      dateCreated: new Date().toISOString()
    };

    const headers = { 'Content-Type': 'application/json' };
    this.http.post("http://localhost:5000/persons", person, {headers: headers, observe: "response"})
      .subscribe(resp => {
          alert('Saved');
        },
        errorResponse => alert(errorResponse.error));

  }

  ngOnInit(): void {
  }

}

interface IPersonResult {
  id: number;
}

interface IPerson {
  name: string,
  dateCreated: string
}
