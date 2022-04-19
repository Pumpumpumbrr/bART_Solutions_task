
# Test task for bART Solutions

This is Wep Api created using .NET 6, EF Core and MS Sql.

## How to run this on your device

Clone this repository

```bash
  git clone https://github.com/Pumpumpumbrr/bART_Solutions_task.git
```

Add Migration in Package Manager Console

```bash
  Add-Migration migrationName
```

Change connection string in appsettings.json

```txt
  "DefaultConnection" :  "your connection string" 
```
Update database

```bash
  Update-Database
```

## How to work with this Web Api

Rules:

```1) You can't create account without contact or contact without account.```

```2) You can't create incident without account or account without incident.```

```3) You can create incident with account which contains contact and after that add new accounts and contacts.```

I added swagger to make it easier to use and test without third-part applications.

So, let's get started:

```1) Create incident using POST /Incidents```

```2) Create new account for this incident using POST /Incidents/addAccount```

```3) Create new contact for any account using POST /Accounts/addContact```

```4) Look at the your data using GET with any controller name```
## Thanks for your attention)


