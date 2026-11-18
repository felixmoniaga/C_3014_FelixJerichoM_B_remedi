using System;
using System.Collections.Generic;

// 1. Class, Object, Attribute, dan Method
class Agen
{
    public string idAgen;
    public string namaAgen;
    public int stokAgen;

    public Agen(string idAgen, string namaAgen, int stokAgen)
    {
        this.idAgen = idAgen;
        this.namaAgen = namaAgen;
        this.stokAgen = stokAgen;
    }

    public void TampilInfo()
    {
        Console.WriteLine("ID Agen   : " + idAgen);
        Console.WriteLine("Nama Agen : " + namaAgen);
        Console.WriteLine("Stok Agen : " + stokAgen);
    }
}

// 2. Encapsulation dan 5. Abstract Class
abstract class ProdukGalon
{
    private string idProduk;
    private string namaProduk;
    private int stokGudang;

    public string IdProduk
    {
        get { return idProduk; }
        set { idProduk = value; }
    }

    public string NamaProduk
    {
        get { return namaProduk; }
        set { namaProduk = value; }
    }

    public int StokGudang
    {
        get { return stokGudang; }
    }

    public ProdukGalon(string idProduk, string namaProduk, int stokGudang)
    {
        this.idProduk = idProduk;
        this.namaProduk = namaProduk;
        
        if (stokGudang < 0)
            this.stokGudang = 0;
        else
            this.stokGudang = stokGudang; 
    }

    public void KurangiStok(int jumlah)
    {
        if (jumlah > 0 && jumlah <= stokGudang)
        {
            stokGudang -= jumlah;
        }
    }

    public abstract int HitungBiaya(int jumlah);
}

// 3. Inheritance dan 4. Polymorphism
class GalonAkua : ProdukGalon
{
    public GalonAkua(string idProduk, string namaProduk, int stokGudang)
        : base(idProduk, namaProduk, stokGudang)
    {
    }

    public override int HitungBiaya(int jumlah)
    {
        return jumlah * 20000;
    }
}

class GalonBerkeran : ProdukGalon
{
    public string jenisKeran;

    public GalonBerkeran(string idProduk, string namaProduk, int stokGudang, string jenisKeran)
        : base(idProduk, namaProduk, stokGudang)
    {
        this.jenisKeran = jenisKeran;
    }

    public override int HitungBiaya(int jumlah)
    {
        return jumlah * 12000;
    }
}

// 6. Interface
interface DapatDidistribusikan
{
    void KirimPaket();
}

class DistribusiGalon : DapatDidistribusikan
{
    public Agen agenTujuan;
    public ProdukGalon produk;
    public int jumlah;

    public DistribusiGalon(Agen agenTujuan, ProdukGalon produk, int jumlah)
    {
        this.agenTujuan = agenTujuan;
        this.produk = produk;
        this.jumlah = jumlah;
    }

    public void KirimPaket()
    {
        if (jumlah <= 0)
        {
            Console.WriteLine("Pengiriman gagal: jumlah galon harus lebih dari 0.");
        }
        else if (jumlah > produk.StokGudang)
        {
            Console.WriteLine("Pengiriman gagal: jumlah melebihi stok gudang yang tersedia.");
        }
        else
        {
            produk.KurangiStok(jumlah);
            agenTujuan.stokAgen += jumlah;
            Console.WriteLine("Paket berhasil dikirimkan ke agen " + agenTujuan.namaAgen
                + " sebanyak " + jumlah + " galon.");
        }
    }
}

// 7. Opsional
// Hubungan SindikatGalon dengan Agen adalah agregation.
// Jadi Agen tetap dapat berdiri sendiri tanpa SindikatGalon.
class SindikatGalon
{
    private List<Agen> daftarAgen = new List<Agen>();
    private List<ProdukGalon> daftarProduk = new List<ProdukGalon>();

    public void TambahAgen(Agen agen)
    {
        daftarAgen.Add(agen);
    }

    public void TambahProduk(ProdukGalon produk)
    {
        daftarProduk.Add(produk);
    }

    public void TampilkanSindikat()
    {
        Console.WriteLine("Daftar Agen");
        foreach (Agen agen in daftarAgen)
        {
            agen.TampilInfo();
            Console.WriteLine();
        }

        Console.WriteLine("Daftar Produk Galon");
        foreach (ProdukGalon produk in daftarProduk)
        {
            Console.WriteLine("ID Produk   : " + produk.IdProduk);
            Console.WriteLine("Nama Produk : " + produk.NamaProduk);
            Console.WriteLine("Stok Gudang : " + produk.StokGudang);
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        Agen agen1 = new Agen("A001", "Agen Jember", 10);
        Agen agen2 = new Agen("A002", "Agen Surabaya", 15);

        Console.WriteLine("Informasi Agen");
        agen1.TampilInfo();
        Console.WriteLine();
        agen2.TampilInfo();

        // 4. Polymorphism: variabel bertipe ProdukGalon
        ProdukGalon galon1 = new GalonAkua("P001", "Air Galon Akua", 100);
        ProdukGalon galon2 = new GalonBerkeran("P002", "Air Galon Berkeran", 80, "Keran Putar");

        Console.WriteLine("\nPolymorphism: Hitung Biaya");
        Console.WriteLine(galon1.NamaProduk + " x5 = Rp" + galon1.HitungBiaya(5));
        Console.WriteLine(galon2.NamaProduk + " x5 = Rp" + galon2.HitungBiaya(5));

        // 6. Distribusi galon
        Console.WriteLine("\nDistribusi Galon");
        DapatDidistribusikan kirim1 = new DistribusiGalon(agen1, galon1, 20);
        DapatDidistribusikan kirim2 = new DistribusiGalon(agen2, galon2, 0);
        DapatDidistribusikan kirim3 = new DistribusiGalon(agen2, galon2, 500);

        kirim1.KirimPaket();
        kirim2.KirimPaket();
        kirim3.KirimPaket();

        // 7. SindikatGalon
        SindikatGalon sindikat = new SindikatGalon();
        sindikat.TambahAgen(agen1);
        sindikat.TambahAgen(agen2);
        sindikat.TambahProduk(galon1);
        sindikat.TambahProduk(galon2);

        Console.WriteLine("\nData Sindikat");
        sindikat.TampilkanSindikat();
    }
}
