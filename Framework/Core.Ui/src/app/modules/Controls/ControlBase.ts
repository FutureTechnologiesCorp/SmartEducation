class ControlBase {
    public Id: string;
    private Value: object;
    private DisplayValue: object;

    constructor(Id: string) {
        if (Id)
            this.Id = Id;
        else
            this.Id = ControlHelper.GetGuid();
    }

    public SetValue(newValue: object): void {
        this.Value = newValue;
    }

    public GetValue(): object {
        return this.Value;
    }
}