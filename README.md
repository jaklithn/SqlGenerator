

## Background

I often found myself in situations where I need to transfer data into my SQL database. The data comes from strange sources and is structured in odd ways. It is relatively easy to shape it up in some kind of table in either Excel or a textfile. But then I need to transfer it into valid SQL ...!

I know SQL syntax and I know how to do search-and-replace, so I can make it. But it is boring and is prone for errors. So I prefer to have a handy tool to do the mapping and generation for me.

## Demo
This is what it looks like when you run the application.

![SqlGenerator demo](Demo.png)

## Features
* Input file can be Excel file, text file or other SQL source
* Sheet names are automatically detected from Excel file
* You can select typical delimeters for text file: comma, tab, semicolon
* You can choose to regard first line as column headers if appropriate
* You can enter any number of connection strings in the application or directly in the settings file
* Connections are displayed with server and database names, hiding the sensitive login information
* Table names are automatically detected from database
* Detailed column information is automatically detected from selected table: name, datatype, size, key, null
* Custom mapping can be performed from file columns to table columns
* Valid choices for each table column are: Map from input column, skip column or enter static data
* If file holds column names they are automatically mapped to table column if names are identical
* Data types are automatically parsed and formatted to valid SQL syntax
* SQL commands can be either: INSERT, UPDATE or DELETE
* Key fields are handled and formed into WHERE clauses as appropriate for UPDATE and DELETE commands
* Generated SQL commands are color coded to make result more readable

## Installation

* Download repository
* Compile the code
* Transfer files from bin/Release directory into any directory of your choice
* Enter required connections strings in the generated settings file or in the application
* Execute





