import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { tap, map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7138/api/auth';

  constructor(private http: HttpClient, private router: Router) { }

  login(username: string, password: string): Observable<{ token: string, message: string }> {
    return this.http.post<{ token: string, message: string }>(`${this.apiUrl}/login`, { username, password })
      .pipe(
        tap(response => {
          console.log('Odpowiedź z serwera:', response);
          if (response && response.token) {
            localStorage.setItem('authToken', response.token);
            localStorage.setItem('currentUser', username);
          } else {
            console.error('Brak tokena w odpowiedzi');
          }
        }),
        catchError(error => {
          console.error('Błąd logowania:', error);
          return of({ token: '', message: 'Login failed' });
        })
      );
  }

  register(username: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, { username, password });
  }

  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('currentUser');
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('authToken');
  }

  getCurrentUser(): string | null {
    return localStorage.getItem('currentUser');
  }
}
