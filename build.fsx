#r "nuget:Fun.Build"

open System.IO
open Fun.Build
open Fun.Result

let (</>) x y = Path.Combine(x, y)

pipeline "test" {
    stage "prepare" {
        run "dotnet tool restore"
        run "dotnet restore"
        run "dotnet build"
    }
    stage "publish" { run "dotnet publish -c Release -o publish" }
    stage "serve" {
        paralle
        workingDir ("publish" </> "wwwroot")
        run "dotnet dotnet-serve -z -b -p 5000"
        run (fun ctx -> async {
            do! Async.Sleep 5000
            do! ctx.OpenBrowser("http://localhost:5000") |> Async.map ignore
        })
    }
    runIfOnlySpecified false
}

tryPrintPipelineCommandHelp ()
