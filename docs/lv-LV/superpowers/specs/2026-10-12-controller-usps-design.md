<!-- generated-doc locale=lv-LV source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO kontroles paplašinājumi

Lietotājs lūdza visas devas ierosinātās uzlabošanas un atpazīstamu tray ikonu. Pastāvošajām konfigurācijām, īpaši G1/G2 spilgtumam, jābūt saderīgām. Pastāvošie neatļauts zīmējums un dokumentācijas redakcijas pieder kopīgam darba telpam un jāsaglabā.

## Ieguldījumi un robežas

1. Brightness darbības izvēlas visus atbalstītos monitorus vai stabilu konkrētu monitoru ID. Nepietiekami izteiktajiem mērķiem ir redzams neveiksme. Dashboard parādīs pašreizējo spilgtumu, iznākuma apjomu, mikrofonu stāvokli un OBS statusu, izmantojot ierobežotus asinkronus.
2. Īpašie atslēgas atbalsta tūlītēju nospiedumu, nospiedumus, turēšanu un dubultus nospiedumiem, ar atkārtotu nospiedšanu. Aizturētā slāņa selektors pagaidu mainās aktīvā mape un atbrīvo to. Pastāvīgi saiknes saglabā savu uzvedību. Pārtraukums, profila izmaiņas, izslēgšanas un atcelšanas skaidri aizņemtie gestus.
3. Darba plūsmas dizainers rediģē nosacījumus, tālākus rādītājus un stop/continue neveiksmes politiku darījumu veidā tīra projektā. Nosacījumi aptver failus, darbības procesus un logus. Nav neierobežotu lēcienu.
4. Izskaidrots ieraksts uzņem īsziņas un gaida, ignorē ievietoto ievadījumu un apstājas uz Escape. Reģistrētas darbības tiek pārskatītas pirms pieņemšanas; parastais teksts nav reģistrēts.
5. Par ierīci standarta īpašie atslēgas no citiem ražotājiem izmanto Windows Raw Input. Īpašuma atslēgas prasa atbalstošu piegādātāja API vai piegādātājas mapēšanu uz standarta atslēgu; nepieņemama funkcionalitāte ir godīgi parādīta. Diagnoze izskaidro profilu, pārtraukumu, saistošus konfliktus un faktisko adaptera stāvokli.
6. Profila apmaiņa izmanto lietotāju izvēlēto kopīgo mapes (tostarp mapes, ko sinhronizē esošais mākoņu klients). Pārbaudes pārbaudes novērš klusu pārrakstīšanu. Pull paredz izmaiņas un importē disabled darbības ar aparatūras mapings noņemts. Darbības saturs ir skaidrs. Neviens mākoņa sertifikāts nav saglabāts MAKRIVO.
7. UI līnijas pieder esošajām 27 valodu katalogiem; DE/EN/FR/IT joprojām ir pārskatītie minimālie dokumentācijas valodas. Tulkošanas nepilnības ir jāatklāj, nevis jāuzskata par pilnīgiem.
8. Atzīmējama M klaviatūras zīme aizstāj ģenētisko Windows trau ikonu. Pausa režīmā ir amber pausa. Izpildāmā / loga zīmējums izmanto to pašu marku. Tīkls ietver statusu un profilu.

## Pārbaude

Kūres pārbaudes aptver atpakaļ saderību, snapshots, atcelšanu, gestu laikā, darba plūsmas / kļūdas, ierakstu filtrēšana un sinhronizācijas konflikti. Windows pārbaudes ietver lasīt vien ekrāna sarakstu, SDK izlaišanas notikumus un Raw Input parsing. WPF smēķēšanas pārbaudes attiecas uz jauniem ieejas punktiem un resursiem, nemainot reālu monitoru spilgtumu, ievadot citā lietojumā vai pārrakstot lietotāju profili. Iestatīšana saglabā rezerves un G1/G2 konfigurāciju.
