# dotnet-external-secrets-demo
Demo dotnet app to illustrate how to leverage external-secrets in a kubernetes environment to fetch secrets from a key vault


## dotnet application
The application in its whole is located in the folder `dotnet-external-secrets-demo` alt av dotnet kode er i Program.cs og docker imaget er definert i Dockerfile i samme mappe.

Appen bygges og publiseres med pipeline `.github/workflows/dotnet-build-and-push.yml`

## kubernetes deployment
Det er laget en deployment mappe som benytter kustomize for å vise muligheten for å definere forskjellige verdier utifra miljø.

`base` mappen innholder basisen som brukes i alle miljøer.
`local` har overstyringer som gjør det mulig å kjøre dette opp lokalt f.eks. i et [kind](https://kind.sigs.k8s.io/) cluster
`prod` er et eksempel på et tenkt prod miljø
`test` er et eksempel på et tenkt test miljø

Det som ikke er satt opp i denne demoen er den faktiske infrastrukturen som trengs i azure og service accounten som er knyttet opp med workload identity til auzre.
Dette må da settes opp i tilleg for at ting skal fungere, bruker du DIS kan man benyttes seg av en `ApplicationIdentity` for å få opprettet service account og en tilstøtende UserAssignedIdentity på azure siden som i sin tur kan grantes de nødvendige rettighetene i en AzureKeyVault.

Når dette er på plass må selvfølgelig også SecretStore objektene oppdateres for at den skal peke på riktig key vault og benytte riktig service account.