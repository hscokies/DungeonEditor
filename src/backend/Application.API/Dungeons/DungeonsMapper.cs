using Application.Dungeons.Edit;
using Domain.Dungeons;
using Riok.Mapperly.Abstractions;

namespace Application.Dungeons;

[Mapper(AllowNullPropertyAssignment = false)]
public partial class DungeonsMapper
{
    public partial EditDungeonCommand ToCommand(DungeonDto dungeon, Guid id, Guid userId);
    public partial void Map(EditDungeonCommand from, Dungeon to);
}
