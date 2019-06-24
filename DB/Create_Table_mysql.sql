 CREATE TABLE Category (
   CategoryName			VARCHAR(40)		NOT NULL,
   CategoryDescription	VARCHAR(255),
   PRIMARY KEY(CategoryName)
   );

CREATE TABLE Instrument
(
	SerialNo		INTEGER			NOT NULL,
	Quantity		INTEGER			NOT NULL,
	Tuning			VARCHAR(3)		NOT NULL,
	InstrumentName	VARCHAR(40)	NOT NULL,
	Price			DECIMAL(8,2)	NOT NULL,
	InstrumentType	CHAR(1)			NOT NULL,
	ModifiedDate	DATETIME		NOT NULL,
	Manufacturer	VARCHAR(80),
	MyCondition		VARCHAR(10),
	CategoryName VARCHAR(40)		NOT NULL,
	FOREIGN KEY (CategoryName) REFERENCES Category(CategoryName) on delete cascade,
    PRIMARY KEY(SerialNo)
	);

CREATE TABLE MyOrder
(
	OrderID		INTEGER 				NOT NULL AUTO_INCREMENT,
	OrderPrice	DECIMAL(8,2)			NOT NULL,
	OrderDate	DATETIME				NOT NULL,
	Quantity	INTEGER					NOT NULL,
	CustName	VARCHAR(80)				NOT NULL,
	CustPhone	VARCHAR(25)				NOT NULL,
	CustMail	VARCHAR(100)			NOT NULL,
	SerialNo	INTEGER					NOT NULL,
	FOREIGN KEY (SerialNo) REFERENCES Instrument(SerialNo),
    PRIMARY KEY (OrderID)
	);
    
   