using SiteManagement.Data;
using SiteManagement.Helpers;
using SiteManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Import
{
    public class ExcelDataImport : Events, IDataImport
    {
        public string InputFileName { get; set; }

        protected ApplicationDbContext _context;

        public ExcelDataImport(ApplicationDbContext context)
        {
            _context = context;
        }

        public StatusInfo Import()
        {
            var result = new StatusInfo(Status.Unknown, "");
            if (!File.Exists(InputFileName))
            {
                result.Status = Status.Fail;
                result.Message = $"Input file {InputFileName} not found.";
                OnError(result.Message);
            }
            var data = ExcelHelper.GetLvData(InputFileName);
            _context.DeleteAll<Lv>();
            _context.DeleteAll<Group>();
            _context.DeleteAll<MeasureUnit>();
            ImportGroups(data);
            ImportMeasureUnits(data);
            ImportLvs(data);

            return result;
        }

        protected int ImportGroups(IEnumerable<LvDto> data)
        {
            var groups = data.Select(x => x.Gruppe).Distinct();
            foreach (var group in groups)
            {
                var item = new GrupGhost(group);

                if (!_context.Groups.Any(x => x.Number == item.Number && x.Name == item.Name) &&
                    !_context.Groups.Local.Any(x => x.Number == item.Number && x.Name == item.Name))
                {
                    _context.Groups.Add(item.ToGroup());

                }
            }
            return _context.SaveChanges();
        }

        protected int ImportMeasureUnits(IEnumerable<LvDto> data)
        {
            var measureUnits = data.Select(x => x.Enih).Distinct();
            foreach(var measureUnit in measureUnits)
            {
                var item = new MeasureUnit
                {
                    Name = measureUnit
                };
                if(!_context.MeasureUnits.Any(x => string.Compare(x.Name, item.Name, StringComparison.Ordinal) == 0) &&
                    !_context.MeasureUnits.Local.Any(x => string.Compare(x.Name, item.Name, StringComparison.Ordinal) == 0))
                {
                    _context.MeasureUnits.Add(item);
                }
            }
            return _context.SaveChanges();
        }

        protected int ImportLvs(IEnumerable<LvDto> data)
        {
            foreach(var item in data)
            {
                if (string.IsNullOrEmpty(item.Bezeichnung)) { continue; }
                decimal ammount;
                if (!decimal.TryParse(item.Menge, out ammount))
                {
                    ammount = 0;
                }
                decimal ep;
                if(!decimal.TryParse(item.Ep, out ep))
                {
                    ep = 0;
                }
                decimal gp;
                if(!decimal.TryParse(item.Gp, out gp))
                {
                    gp = 0;
                }

                if(gp == 0)
                {
                    gp = ep * ammount;
                }
                var lv = new Lv
                {
                    PosNr = item.PosNr,
                    GroupId = GetGroupId(item.Gruppe),
                    Name = item.Bezeichnung,
                    Ammount = ammount,
                    MeasureUnitId = _context.MeasureUnits.FirstOrDefault(x => x.Name == item.Enih).Id,
                    Ep = ep,
                    Gp = gp
                };
                _context.Lvs.Add(lv);
            }
            return _context.SaveChanges();
        }

        protected int GetGroupId(string field)
        {
            var ghost = new GrupGhost(field);
            var grup = _context.Groups.FirstOrDefault(x => x.Name == ghost.Name && x.Number == ghost.Number);
            return grup.Id;
        }
    }

    public class GrupGhost
    {
        public int Number { get; set; }
        public string Name { get; set; }

        public GrupGhost(string field)
        {
            var number = -1;
            var name = field;
            var spaceIndex = field.IndexOf(" ");
            if (spaceIndex >= 0)
            {
                var strNumber = field.Substring(0, spaceIndex).Trim();
                if (!int.TryParse(strNumber, out number))
                {
                    number = -1;
                }
                name = field.Substring(spaceIndex + 1).Trim();
            }
            Name = name;
            Number = Number;
        }

        public GrupGhost(Group group)
        {
            Number = group.Number;
            Name = group.Name;
        }

        public Group ToGroup()
        {
            return new Group
            {
                Name = this.Name,
                Number = this.Number
            };
        }
    }
}
