const MONTH_NAMES = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December",
];
const DAYS = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

function app() {
    return {
        month: "",
        year: "",
        idcliente: 0,
        no_of_days: [],
        blankdays: [],
        days: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        events: [],
        event_title: "",
        event_date: "",
        event_theme: "blue",
        themes: [
            {
                value: "blue",
                label: "Blue Theme",
            },
            {
                value: "red",
                label: "Red Theme",
            },
            {
                value: "yellow",
                label: "Yellow Theme",
            },
            {
                value: "green",
                label: "Green Theme",
            },
            {
                value: "purple",
                label: "Purple Theme",
            },
        ],
        openEventModal: false,
        initDate(id) {
            console.log("Solo una vez")
            this.idcliente = id;
            let today = new Date();
            this.month = today.getMonth();
            this.year = today.getFullYear();
            this.datepickerValue = new Date(
                this.year,
                this.month,
                today.getDate()
            ).toDateString();
        },
        setIdCliente(id) {
            console.log("ID: " + id)
        },
        isToday(date) {
            const today = new Date();
            const d = new Date(this.year, this.month, date);
            return today.toDateString() === d.toDateString() ? true : false;
        },
        showEventModal(date) {
            // open the modal
            this.openEventModal = true;
            this.event_date = new Date(this.year, this.month, date).toDateString();
        },
        async addEvent() {
            var mes = 0;
            console.log("Mes var: " + this.month)
            mes = this.month + 1;
            console.log("Mes var despues: " + mes)
            console.log(this.idcliente + "-" + mes)
            $("#loading-calendar").show();
            $("#content-calendar").hide();
            fetch("/Entrenador/EventosMes?mes=" + mes + "&idcliente=" + this.idcliente)
                .then(res => res.json())
                .then(data => {
                    console.log(data)
                    this.events = []
                    for (d of data) {
                        var fecha = new Date(d.fecha);
                        this.events.push({
                            event_date: fecha,
                            event_title: d.nombre,
                            event_theme: "blue",
                        });
                    }
                    $("#loading-calendar").hide();
                    $("#content-calendar").show();
                });
        },
        getNoOfDays(operacion) {
            console.log(operacion)
            if (operacion == 1) {
                console.log("Entro resto")
                if (this.month - 1 < 0) {
                    this.month = 11;
                    this.year--;
                    console.log("Reinicio resto")
                } else {
                    console.log("Resto")
                    this.month--;
                }
                this.addEvent()
            } else if (operacion == 2) {
                console.log("Entro suma")
                if (this.month + 1 > 11) {
                    this.month = 0;
                    this.year++;
                    console.log("Reinicio suma")
                } else {
                    console.log("Sumo")
                    this.month++;
                }
                this.addEvent()
            } else if (operacion == 0) {
                this.addEvent()
            }
            console.log("Mes var2: " + this.month)
            let daysInMonth = new Date(this.year, this.month + 1, 0).getDate();
            console.log("DayInMonth: " + daysInMonth)
            // find where to start calendar day of week
            let dayOfWeek = new Date(this.year, this.month).getDay();
            //console.log("DayOfWeek: " + dayOfWeek)
            let blankdaysArray = [];
            for (var i = 1; i <= dayOfWeek; i++) {
                blankdaysArray.push(i);
            }
            //console.log("BlankdaysArray: " + blankdaysArray)
            let daysArray = [];
            for (var i = 1; i <= daysInMonth; i++) {
                daysArray.push(i);
            }
            //console.log("DayArray: " + daysArray)
            this.blankdays = blankdaysArray;
            this.no_of_days = daysArray;
            //console.log("No of days: " + this.no_of_days)
            console.log("------------------------------------")
        },
    };
}
