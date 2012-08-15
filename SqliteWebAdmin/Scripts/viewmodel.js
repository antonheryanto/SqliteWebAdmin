function ViewModel() {
    self = this;
    self.file = ko.observable();
    self.sql = ko.observable();
    self.message = ko.observable();
    self.tables = ko.observableArray([]);
    self.table = ko.observable();
    self.cols = ko.observableArray([]);
    self.rows = ko.observableArray([]);
    self.getTable = ko.computed(function () {
        if (self.file() === undefined) return;
        $.getJSON("/Home/Tables", { file: self.file() }, function (data) {
            self.tables(data);
        });
        self.empty();        
    });
    self.query = function () {
        self.empty();
        $.getJSON("/Home/Query", { file: self.file(), sql: self.sql() }, function (data) {
            if (data != null && data.cols != null) {
                self.cols(data.cols);
                self.rows(data.rows);
            } else {
                self.message(data.message);
            }
        });
    };
    self.queryTable = function (data) {
        self.table(data);
        self.sql("SELECT * FROM " + data);
        self.query();
    }
    self.browseTable = function (data) {
        self.queryTable(self.table());
    }
    self.tableSchema = function (data) {
        self.sql("PRAGMA table_info(" + self.table() + ")");
        self.query();
    }
    self.empty = function empty() {
        self.message(undefined);
        self.cols([]);
        self.rows([]);
    }
}
ko.applyBindings(new ViewModel());