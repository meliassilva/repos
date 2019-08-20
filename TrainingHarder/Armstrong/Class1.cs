//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Armstrong
//{
//    class Class1
//    {

//        public void AddGroup(GroupNodeViewModel group)
//        {
//            var person = Context.GetQueryable<Person>()
//                .SingleOrDefault(p => p.Id == group.AssignedPerson.Id);

//            if (person != null)
//            {
//                if (group.GroupNodeTypeId == (int)GroupNodeType.Oim)
//                {
//                    var oimGroupNode = new OimGroupNode()
//                    {
//                        Term = Context.Terms().SingleResource(t => t.Id == group.TermId)
//                    };

//                    oimGroupNode = oimGroupNode.Assign(person) as OimGroupNode;

//                    Context.GetSet<OimGroupNode>().Add(oimGroupNode);
//                }
//                else if (group.GroupNodeTypeId == (int)GroupNodeType.Aim)
//                {
//                    var aimGroupNode = new AimGroupNode
//                    {
//                        Term = Context.Terms().SingleResource(t => t.Id == group.TermId),
//                        OimGroupNode = Context.GetQueryable<OimGroupNode>().Single(g => g.Id == group.ParentGroupNodeId)
//                    };
//                    aimGroupNode = aimGroupNode.Assign(person) as AimGroupNode;
//                    Context.GetSet<AimGroupNode>().Add(aimGroupNode);
//                }
//                else if (group.GroupNodeTypeId == (int)GroupNodeType.Tgl)
//                {
//                    var tglGroupNode = new TglGroupNode()
//                    {
//                        Term = Context.Terms().SingleResource(t => t.Id == group.TermId),
//                        Name = group.Name,
//                        AimGroupNode = Context.GetQueryable<AimGroupNode>().Single(g => g.Id == group.ParentGroupNodeId)
//                    };
//                    tglGroupNode = tglGroupNode.Assign(person) as TglGroupNode;
//                    Context.GetSet<TglGroupNode>().Add(tglGroupNode);
//                }
//                else
//                {
//                    Context.GetSet<InstructorGroupNode>().Add(new InstructorGroupNode()
//                    {
//                        AssignedPerson = group.AssignedPerson != null ?
//                            person : null,
//                        Term = Context.Terms().SingleResource(t => t.Id == group.TermId),
//                        TglGroupNode = Context.GetQueryable<TglGroupNode>().Single(g => g.Id == group.ParentGroupNodeId)
//                    });
//                }
//                Context.SaveChanges();
//            }
//        }
//    }
