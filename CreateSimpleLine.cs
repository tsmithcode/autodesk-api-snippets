[CommandMethod("CreateLine")]
public void CreateSimpleLine()
{
    Document acDoc = Application.DocumentManager.MdiActiveDocument;
    Database acCurDb = acDoc.Database;
    Editor acEd = acDoc.Editor;

    // Start a transaction
    using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
    {
        // Open the BlockTable for read to get the ModelSpace BlockTableRecord
        BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

        // Open the Model Space BlockTableRecord for write
        BlockTableRecord acMsBlkRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

        // Create a new Line object
        Line acLine = new Line(new Point3d(0, 0, 0), new Point3d(10, 10, 0));

        // Add the new object to Model Space and the transaction
        acMsBlkRec.AppendEntity(acLine);
        acTrans.AddNewlyCreatedDBObject(acLine, true); // true = add to graphics

        // Commit the transaction
        acTrans.Commit();
        acEd.WriteMessage("\nLine created successfully!");
    } // The 'using' block ensures the transaction is properly disposed (committed or aborted)
}
