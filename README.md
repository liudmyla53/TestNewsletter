Ce que j'ai réalisé (Ce qui a été fait) :

    Architecture REST API : J'ai conçu des points de terminaison (Endpoints) structurés pour l'inscription et la désinscription.

    Persistance avec SQL Server : J'ai intégré Entity Framework Core pour transformer mes objets C# en tables de base de données réelles.

    Gestion des données complexes : J'ai configuré une conversion JSON pour stocker des listes (List<string>) dans une colonne SQL, ce qui prouve une maîtrise technique avancée.

    Idempotence : Mon système est intelligent ; il ne crée pas de doublons si un utilisateur s'inscrit plusieurs fois, il met simplement à jour ses préférences.

    Conformité RGPD : J'ai implémenté le droit à l'oubli via la méthode DELETE, supprimant toutes les traces de l'utilisateur en base de données.

    Communication SMTP : J'ai intégré la bibliothèque MailKit pour envoyer des emails automatiques, connectée à un serveur de test (Smtp4Dev).

    Migrations de base de données : J'ai utilisé les outils CLI (Add-Migration et Update-Database) pour gérer l'évolution du schéma de données.

    Documentation et Tests : J'ai utilisé Scalar (OpenAPI) pour documenter et tester mon API visuellement.

