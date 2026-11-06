// mas ini no 1 yaa mas yaa
class Sekolah
{
    public string IdSekolah { get; set; }
    public string NamaSekolah { get; set; }
    public int JumlahPaketDiterima { get; set; }

    public Sekolah(string id, string nama)
    {
        IdSekolah = id;
        NamaSekolah = nama;
        JumlahPaketDiterima = 0;
    }

    public void TampilInfo()
    {
        Console.WriteLine($"INI DATA SEKOLAH WOI\nid: {IdSekolah}\nnama: {NamaSekolah}\npaket diterima: {JumlahPaketDiterima}");
    }
}

// mas ini no 2 include sama no 5 yaa
abstract class PaketMBG
{
    private string _idPaket;
    private string _namaPaket;
    private int _stokDapur;

    public string IdPaket
    {
        get { return _idPaket; }
        set { _idPaket = value; }
    }

    public string NamaPaket
    {
        get { return _namaPaket; }
        set { _namaPaket = value; }
    }

    public int StokDapur
    {
        get { return _stokDapur; }
        set
        {
            if (value < 0)
                Console.WriteLine("jumlah stok gabole minus");
            else
                _stokDapur = value;
        }
    }

    public PaketMBG(string id, string nama, int stok)
    {
        IdPaket = id;
        NamaPaket = nama;
        StokDapur = stok;
    }

    public void KurangiStok(int jumlah)
    {
        if (jumlah <= 0)
        {
            Console.WriteLine("jumlah pengurangan harus lebih dari 0");
            return;
        }
        if (jumlah > _stokDapur)
        {
            Console.WriteLine("jumlah pengurangan melebihi stok yang tersedia.");
            return;
        }
        _stokDapur -= jumlah;
    }

    public abstract double HitungBiaya(int jumlah);

    public virtual void TampilInfo()
    {
        Console.WriteLine($"INI DATA PAKET WOI\nid: {IdPaket} | nama: {NamaPaket} | stok: {StokDapur}");
    }
}

// mas ini no 3 sama no 4 sekalian yaaa
class PaketReguler : PaketMBG
{
    public PaketReguler(string id, string nama, int stok)
        : base(id, nama, stok) { }

    public override double HitungBiaya(int jumlah)
    {
        return jumlah * 15000;
    }

    public override void TampilInfo()
    {
        base.TampilInfo();
        Console.WriteLine($"harga: Rp15.000/paket");
    }
}

class PaketKhusus : PaketMBG
{
    public string JenisDiet { get; set; }

    public PaketKhusus(string id, string nama, int stok, string jenisDiet)
        : base(id, nama, stok)
    {
        JenisDiet = jenisDiet;
    }

    public override double HitungBiaya(int jumlah)
    {
        return jumlah * 20000;
    }

    public override void TampilInfo()
    {
        base.TampilInfo();
        Console.WriteLine($"diet: {JenisDiet}\nharga: Rp20.000/paket");
    }
}

// ini no 5 yaa masss
interface DapatDidistribusikan
{
    void KirimPaket();
}

class DistribusiMBG : DapatDidistribusikan
{
    private Sekolah _sekolah;
    private PaketMBG _paket;
    private int _jumlah;

    public DistribusiMBG(Sekolah sekolah, PaketMBG paket, int jumlah)
    {
        _sekolah = sekolah;
        _paket = paket;
        _jumlah = jumlah;
    }

    public void KirimPaket()
    {
        if (_jumlah <= 0)
        {
            Console.WriteLine("jumlah paket harus lebih dari 0");
            return;
        }

        if (_jumlah > _paket.StokDapur)
        {
            Console.WriteLine($"stok tidak cukup (stok tersedia: {_paket.StokDapur}).");
            return;
        }

        _paket.KurangiStok(_jumlah);
        _sekolah.JumlahPaketDiterima += _jumlah;
        Console.WriteLine($"distribusi sukses, sebanyak {_jumlah} paket makanan telah dikirim ke sekolah {_sekolah.NamaSekolah}.");
    }
}

// mas ini no 7 yaa
class PengelolaMBG
{
    private List<Sekolah> daftarSekolah = new List<Sekolah>();
    private List<PaketMBG> daftarPaket = new List<PaketMBG>();

    public void TambahSekolah(Sekolah sekolah)
    {
        daftarSekolah.Add(sekolah);
        Console.WriteLine($"sekolah '{sekolah.NamaSekolah}' berhasil didaftarkan");
    }

    public void TambahPaket(PaketMBG paket)
    {
        daftarPaket.Add(paket);
        Console.WriteLine($"paket '{paket.NamaPaket}' berhasil ditambahkan");
    }

    public void TampilkanDataMBG()
    {
        Console.WriteLine("\n===== DATA SEKOLAH PENERIMA MBG =====");
        foreach (var s in daftarSekolah)
            s.TampilInfo();

        Console.WriteLine("\n===== DATA PAKET MAKANAN =====");
        foreach (var p in daftarPaket)
            p.TampilInfo();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========== PROGRAM MBG ==========\n");

        Sekolah s1 = new Sekolah("SKLH001", "SDN Sukamakmur 01");
        Sekolah s2 = new Sekolah("SKLH002", "SDN Jember Lor 03");
        s1.TampilInfo();
        s2.TampilInfo();

        PaketReguler paketA = new PaketReguler("PKT001", "paket reguler harian", 100);
        PaketKhusus paketB = new PaketKhusus("PKT002", "paket vegetarian", 50, "vegetarian");

        Console.WriteLine();
        paketA.TampilInfo();
        paketB.TampilInfo();

        Console.WriteLine("\n--- Hitung Biaya ---");
        PaketMBG[] daftarPaketPoly = { paketA, paketB };
        foreach (PaketMBG p in daftarPaketPoly)
        {
            double biaya = p.HitungBiaya(10);
            Console.WriteLine($"{p.NamaPaket}: 10 paket = Rp{biaya:N0}");
        }

        Console.WriteLine("\n--- Distribusi Paket ---");
        DistribusiMBG dist1 = new DistribusiMBG(s1, paketA, 30);
        dist1.KirimPaket(); // sukses

        DistribusiMBG dist2 = new DistribusiMBG(s2, paketB, 200); // melebihi stok
        dist2.KirimPaket(); // gagal

        DistribusiMBG dist3 = new DistribusiMBG(s2, paketB, 0); // jumlah 0
        dist3.KirimPaket(); // gagal

        Console.WriteLine("\n--- Pengelola MBG ---");
        PengelolaMBG pengelola = new PengelolaMBG();
        pengelola.TambahSekolah(s1);
        pengelola.TambahSekolah(s2);
        pengelola.TambahPaket(paketA);
        pengelola.TambahPaket(paketB);
        pengelola.TampilkanDataMBG();
    }
}                       