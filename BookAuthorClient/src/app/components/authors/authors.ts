import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
@Component({
  selector: 'app-authors',
  imports: [RouterOutlet],
  templateUrl: './authors.html',
  styleUrl: './authors.css',
})
export class Authors implements OnInit {
  private http = inject(HttpClient);  // HttpClient'i component'te kullanabilmek için enjekte eder
  protected readonly title = signal('NorthwindEntities'); //signal ile Angular'da reaktif bir veri yapısı oluşturur. signal yapısı, bir değeri izler ve bu değerin değişmesi durumunda component'i yeniden render eder.


  protected authors = signal<any>([]); //authors sinyali, yazarlara ait veriyi tutmak için kullanılan bir reaktif yapıdır. başta boş bir dizi
  protected selectedAuthor = signal<any | null>(null); // seçilen bir yazarı tutar. Kullanıcı bir yazar seçtiğinde, bu sinyale seçilen yazarın bilgileri atanabilir.


  async ngOnInit() { // ngOnInit, component ilk defa başlatıldığında çağrılır. Bu metod, verileri almak ve başlatmak için yaygın olarak kullanılır.

    this.authors.set(await this.getAuthors());
  }

  async getAuthors(): Promise<Object> { // bu fonksiyon promise döner.
    try {
      return lastValueFrom(this.http.get('http://localhost:5277/api/Author')); 
      // lastValueFrom->  HttpClient.get()'ten dönen Observable'ı Promise'e dönüştürür
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

  protected newFirstName = signal<string>(''); //signal sayesinde, veriler değiştiğinde component otomatik olarak güncellenir.
  protected newLastName = signal<string>('');
  // Bu sinyaller, kullanıcı tarafından girilen yazar adı ve soyadı değerlerini tutar

  onNewFirstNameChange(event: Event): void {
    // Kullanıcı, "Yazar İsmi" input alanına bir şeyler yazdıkça bu fonksiyon çağrılır. Yazılan değeri, newFirstName sinyaline aktarır
    const value = (event.target as HTMLInputElement).value; // Bu satır, input elemanının şu anki değerini alır.
    this.newFirstName.set(value); // Kullanıcı tarafından girilen değeri newFirstName sinyaline atar.
  }

  onNewLastNameChange(event: Event): void {
    // Kullanıcı "Yazar Soyadı" input alanına yazılan değeri alır ve newLastName sinyaline aktarır.
    const value = (event.target as HTMLInputElement).value; // Bu satır, input elemanının şu anki değerini alır.
    this.newLastName.set(value);  // Kullanıcı tarafından girilen değeri newLastName sinyaline atar.
  }

  async addAuthors(event: Event) {
  
    event.preventDefault(); // Angular SPA ile çalıştığı için, bu satır  sayfanın yeniden yüklenmesini engeller.

    const body = { // body adında bir nesne oluşturulur
      FirstName: this.newFirstName(),
      LastName: this.newLastName(),
      // sinyallerden alınan değerler bu nesneye atanır
    };

    try {
      const message = await lastValueFrom(
        // Angular'daki HTTP istekleri Observable döndürür. lastValueFrom kullanılarak bu observable, bir Promise'e dönüştürülür. Bu sayede await ile asenkron olarak çalışabilir
        this.http.post('http://localhost:5277/api/Author', body, { // ekleme olduğu için post isteği, body-> Form verilerinin bulunduğu nesne
          responseType: 'text',  // backend'den dönen cevabın tipidir. metin yani string belirlendiği için burada text olur.
        })
      );

      console.log("API mesajı ",message);
      this.authors.set(await this.getAuthors()); // yeni yazar eklendiğinde dinamik olarak güncellenmesi için tekrar bu fonk çağrılır

      this.newFirstName.set(''); // formdaki input alanı sıfırlanır
      this.newLastName.set(''); // formdaki input alanı sıfırlanır

    } catch (error) {
      console.log(error);
    }
  }
}
