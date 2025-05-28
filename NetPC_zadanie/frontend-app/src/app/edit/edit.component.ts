import { Component } from '@angular/core';
import { Contact, ContactService } from '../services/contact.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit',
  imports: [CommonModule, FormsModule],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.scss'
})


export class EditComponent {

  categories = ['Work', 'Private', 'Other'];
  subCategories = ['Boss', 'Client'];

  contact: Contact = {
    id: 0,
    name: '',
    surname: '',
    password: '',
    email: '',
    phone: '',
    birthDate: '',
    category: '',
    subCategory: ''
  };

  constructor(private route: ActivatedRoute, private router: Router, private contactService: ContactService ) { }

  onSubmit() {
    const dto = {
      name: this.contact.name,
      surname: this.contact.surname,
      password: this.contact.password,
      email: this.contact.email,
      phone: this.contact.phone,
      birthDate: this.contact.birthDate,
      category: typeof this.contact.category === 'number'
        ? this.categories[this.contact.category]
        : this.contact.category,
      subCategory: typeof this.contact.subCategory === 'number'
        ? this.subCategories[this.contact.subCategory]
        : this.contact.subCategory
    };

    console.log('Dane wysyłane do PUT:', dto);

    this.contactService.updateContact(this.contact.id, dto).subscribe({
      next: () => {
        console.log("Contact saved");
        //this.router.navigate(['/contacts', this.contact.id]);
      },
      error: (err) => console.error("Error during saving contact", err)
    });
  }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.contactService.getContactById(id).subscribe({
      next: (data) =>
      {
        if (data.birthDate)
        {
          const date = new Date(data.birthDate);
          const yyyy = date.getFullYear();
          const mm = String(date.getMonth() + 1).padStart(2, '0');
          const dd = String(date.getDate()).padStart(2, '0');
          data.birthDate = `${yyyy}-${mm}-${dd}`;
        }
        this.contact = { ...data };
        console.log('Dane kontaktu:', this.contact);
      },
      error: (err) => console.error('Błąd pobierania kontaktu:', err)
    });
  }
  goToContacts() {
    this.router.navigate(['/contacts']);
  }
}
