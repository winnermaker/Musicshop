INSERT INTO category values ("Brass","A brass instrument is a musical instrument that produces sound by sympathetic vibration of air in a tubular resonator in sympathy with the vibration of the players lips.");
INSERT INTO category values ("Woodwind","Woodwind instruments are a family of musical instruments within the more general category of wind instruments. There are two main types of woodwind instruments: flutes and reed instruments (otherwise called reed pipes).");
INSERT INTO category values ("Percussion","A percussion instrument is a musical instrument that is sounded by being struck or scraped by a beater struck, scraped or rubbed by hand or struck against another similar instrument.");

INSERT INTO Instrument values (123,10,"b","Trumpet YT1234",1367.23,"N","2019-06-22 13:42:45","Yamaha",null,"Brass");
INSERT INTO Instrument values (124,15,"c","Trombone YT577",4527.50,"N","2019-06-22 13:42:45","Yamaha",null,"Brass");
INSERT INTO Instrument values (125,15,"c","Trombone YT5",4527.50,"U","2019-06-22 13:42:45",null,"as new","Brass");

INSERT INTO MyOrder (OrderPrice, OrderDate, Quantity, CustName, CustPhone, CustMail, SerialNo) values (1367.23,"2019-06-22",1,"Dominik Thoma","0221176182","thoma@hotmail.com",123);
INSERT INTO MyOrder (OrderPrice, OrderDate, Quantity, CustName, CustPhone, CustMail, SerialNo) values (9055.00,"2019-06-22",2,"Dominik Thoma","0221176182","thoma@hotmail.com",124);