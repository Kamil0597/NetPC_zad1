import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
export interface Contact {
  id: number;
  name: string;
  surname: string;
  password: string;
  email: string;
  phone: string;
  birthDate: string;
  category: string;
  subCategory?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private apiUrl = 'https://localhost:7138/api/contacts';

  constructor(private http: HttpClient) { }

  getAllContacts(): Observable<Contact[]>
  {
    return this.http.get<Contact[]>(this.apiUrl);
  }

  getContactById(id: number): Observable<Contact>
  {
    return this.http.get<Contact>(`${this.apiUrl}/${id}`);
  }

  updateContact(id: number, contact: Contact): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, contact);
  }

  createContact(contact: Contact): Observable<Contact> {
    const token = localStorage.getItem('authToken');  // Pobranie tokena JWT
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post<Contact>(this.apiUrl, contact, { headers }).pipe(
      catchError(err => {
        if (err.status === 409) {
          return throwError(() => new Error('E-mail already exists.'));
        } else if (err.status === 401) {
          return throwError(() => new Error('Unauthorized. Please log in.'));
        } else {
          return throwError(() => new Error('An error occurred while creating contact.'));
        }
      })
    );
  }
}
