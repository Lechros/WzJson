﻿using WzJson.Model;

namespace WzJson.Domain;

public enum GearPropType
{
    //普通装备属性
    incSTR = 1,
    incSTRr,
    incDEX,
    incDEXr,
    incINT,
    incINTr,
    incLUK,
    incLUKr,
    incAllStat_incMHP25,
    incAllStat_incMHP50_incMMP50,
    incAllStat,
    incMHP_incMMP,
    incMHPr_incMMPr,
    incMHP,
    incMHPr,
    incMMP,
    incMMPr,
    incMDF,
    incARC,
    incAUT,
    incPAD_incMAD,
    incPAD,
    incMAD,
    incAD,
    incPDD_incMDD,
    incPDD,
    incMDD,
    incACC_incEVA,
    incACC,
    incEVA,
    incSpeed,
    incJump,
    incCraft,
    knockback,
    incPVPDamage,
    bdR,
    incBDR,
    imdR,
    incIMDR,
    damR,
    nbdR,
    statR,
    incCHUC,

    //潜能属性
    incPADr = 100,
    incMADr,
    incPDDr,
    incMDDr,
    incACCr,
    incEVAr,
    incCr,
    incCDr,
    incDAMr,
    RecoveryHP,
    RecoveryMP,
    face,
    prop,
    time,
    HP,
    MP,
    attackType,
    ignoreTargetDEF,
    ignoreDAM,
    ignoreDAMr,
    DAMreflect,
    mpconReduce,
    mpRestore,
    incMesoProp,
    incRewardProp,
    incAllskill,
    RecoveryUP,
    boss,
    level,
    incTerR,
    incAsrR,
    incEXPr,
    reduceCooltime,
    incCriticaldamageMax,
    incCriticaldamageMin,
    @sealed,
    incSTRlv,
    incDEXlv,
    incINTlv,
    incLUKlv,
    incMaxDamage,
    incMHPlv,
    incPADlv,
    incMADlv,
    incCriticaldamage,

    Option,
    OptionToMob,
    activeSkill,
    bonusByTime,

    //特殊装备属性
    attackSpeed = 200,
    tuc,
    setItemID,
    durability,
    reqCraft,
    cash,
    royalSpecial,
    masterSpecial,
    reduceReq,

    //技能特有属性
    mastery = 300,

    //criticaldamageMin,
    //criticaldamageMax,
    criticaldamage,
    epad,
    emad,
    epdd,
    emdd,
    emhp,
    emmp,
    smartpad,
    smartacc,
    smarteva,

    //装备特有属性
    reqLevel = 1000,
    reqSTR,
    reqDEX,
    reqINT,
    reqLUK,
    reqJob,
    reqPOP,
    reqSpecJob,
    reqWeekDay, //要求日子
    grade,

    only = 1100,
    //notSale,
    //dropBlock,
    tradeBlock,
    accountSharable,
    onlyEquip,
    tradeAvailable,
    equipTradeBlock,
    sharableOnce,
    notExtend,
    epicItem,
    charismaEXP,
    senseEXP,
    insightEXP,
    willEXP,
    craftEXP,
    charmEXP,
    cashForceCharmExp,
    accountShareTag,
    noPotential,
    fixedPotential,
    timeLimited,
    specialGrade,
    fixedGrade,
    unchangeable,
    superiorEqp,
    incPQEXPr,
    limitBreak,
    nActivatedSocket,
    jokerToSetItem,
    medalTag,
    ringOptionSkill,
    ringOptionSkillLv,
    abilityTimeLimited,
    blockGoldHammer,
    exceptUpgrade,
    colorvar,
    noMoveToLocker,
    onlyUpgrade,
    cantRepair,
    noPetEquipStatMoveItem,
    BTSLabel,
    BLACKPINKLabel,
    android,
    noLookChange,
    tucIgnoreForPotential,
    Etuc,
    CuttableCount,
    exUpgradeBlock,
    exUpgradeChangeBlock,

    gatherTool_incSkillLevel = 2000,
    gatherTool_incSpeed,
    gatherTool_incNum,
    gatherTool_reqSkillLevel,
    
    
    bossReward = 9999, // Custom
}

public static class GearPropTypeExtensions
{
    public static string? GetGearOptionName(this GearPropType propType)
    {
        return propType switch
        {
            GearPropType.incSTR => nameof(GearOption.Str),
            GearPropType.incSTRr => nameof(GearOption.StrRate),
            GearPropType.incDEX => nameof(GearOption.Dex),
            GearPropType.incDEXr => nameof(GearOption.DexRate),
            GearPropType.incINT => nameof(GearOption.Int),
            GearPropType.incINTr => nameof(GearOption.IntRate),
            GearPropType.incLUK => nameof(GearOption.Luk),
            GearPropType.incLUKr => nameof(GearOption.LukRate),
            GearPropType.incAllStat_incMHP25 => null,
            GearPropType.incAllStat_incMHP50_incMMP50 => null,
            GearPropType.incAllStat => nameof(GearOption._AllStatsSetter),
            GearPropType.incMHP_incMMP => null,
            GearPropType.incMHPr_incMMPr => null,
            GearPropType.incMHP => nameof(GearOption.MaxHp),
            GearPropType.incMHPr => nameof(GearOption.MaxHpRate),
            GearPropType.incMMP => nameof(GearOption.MaxMp),
            GearPropType.incMMPr => nameof(GearOption.MaxMpRate),
            GearPropType.incMDF => nameof(GearOption.MaxDemonForce),
            GearPropType.incARC => null,
            GearPropType.incAUT => null,
            GearPropType.incPAD_incMAD => null,
            GearPropType.incPAD => nameof(GearOption.AttackPower),
            GearPropType.incMAD => nameof(GearOption.MagicPower),
            // GearPropType.incAD => null,
            GearPropType.incPDD_incMDD => null,
            GearPropType.incPDD => nameof(GearOption.Armor),
            GearPropType.incMDD => null,
            GearPropType.incACC_incEVA => null,
            GearPropType.incACC => null,
            GearPropType.incEVA => null,
            GearPropType.incSpeed => nameof(GearOption.Speed),
            GearPropType.incJump => nameof(GearOption.Jump),
            GearPropType.incCraft => null,
            GearPropType.knockback => nameof(GearOption.Knockback),
            GearPropType.incPVPDamage => null,
            GearPropType.bdR => nameof(GearOption.BossDamage),
            GearPropType.incBDR => nameof(GearOption.BossDamage),
            GearPropType.imdR => nameof(GearOption.IgnoreMonsterArmor),
            GearPropType.incIMDR => nameof(GearOption.IgnoreMonsterArmor),
            GearPropType.damR => nameof(GearOption.Damage),
            GearPropType.nbdR => null, // TODO: Add nbdR to GearOption
            // GearPropType.statR => null,
            GearPropType.incCHUC => null,
            GearPropType.incPADr => nameof(GearOption.AttackPowerRate),
            GearPropType.incMADr => nameof(GearOption.MagicPowerRate),
            GearPropType.incPDDr => nameof(GearOption.ArmorRate),
            GearPropType.incMDDr => null,
            GearPropType.incACCr => null,
            GearPropType.incEVAr => null,
            GearPropType.incCr => nameof(GearOption.CriticalRate),
            GearPropType.incCDr => nameof(GearOption.CriticalDamage),
            GearPropType.incDAMr => nameof(GearOption.Damage),
            GearPropType.RecoveryHP => null,
            GearPropType.RecoveryMP => null,
            GearPropType.face => null,
            GearPropType.prop => null,
            GearPropType.time => null,
            GearPropType.HP => null,
            GearPropType.MP => null,
            GearPropType.attackType => null,
            GearPropType.ignoreTargetDEF => nameof(GearOption.IgnoreMonsterArmor),
            GearPropType.ignoreDAM => null,
            GearPropType.ignoreDAMr => null,
            GearPropType.DAMreflect => null,
            GearPropType.mpconReduce => null,
            GearPropType.mpRestore => null,
            GearPropType.incMesoProp => null,
            GearPropType.incRewardProp => null,
            GearPropType.incAllskill => null,
            GearPropType.RecoveryUP => null,
            GearPropType.boss => null,
            GearPropType.level => null,
            GearPropType.incTerR => null,
            GearPropType.incAsrR => null,
            GearPropType.incEXPr => null,
            GearPropType.reduceCooltime => nameof(GearOption.CooltimeReduce),
            // GearPropType.incCriticaldamageMax => null,
            // GearPropType.incCriticaldamageMin => null,
            GearPropType.@sealed => null,
            GearPropType.incSTRlv => nameof(GearOption.StrLv),
            GearPropType.incDEXlv => nameof(GearOption.DexLv),
            GearPropType.incINTlv => nameof(GearOption.IntLv),
            GearPropType.incLUKlv => nameof(GearOption.LukLv),
            GearPropType.incMaxDamage => null,
            GearPropType.incMHPlv => null,
            GearPropType.incPADlv => null,
            GearPropType.incMADlv => null,
            GearPropType.incCriticaldamage => nameof(GearOption.CriticalDamage),
            GearPropType.Option => null,
            GearPropType.OptionToMob => null,
            GearPropType.activeSkill => null,
            GearPropType.bonusByTime => null,
            GearPropType.attackSpeed => null,
            GearPropType.tuc => null,
            GearPropType.setItemID => null,
            GearPropType.durability => null,
            GearPropType.reqCraft => null,
            GearPropType.cash => null,
            GearPropType.royalSpecial => null,
            GearPropType.masterSpecial => null,
            GearPropType.reduceReq => nameof(GearOption.ReqLevelDecrease),
            GearPropType.mastery => null,
            GearPropType.criticaldamage => nameof(GearOption.CriticalDamage),
            GearPropType.epad => null,
            GearPropType.emad => null,
            GearPropType.epdd => null,
            GearPropType.emdd => null,
            GearPropType.emhp => null,
            GearPropType.emmp => null,
            GearPropType.smartpad => null,
            GearPropType.smartacc => null,
            GearPropType.smarteva => null,
            GearPropType.reqLevel => null,
            GearPropType.reqSTR => null,
            GearPropType.reqDEX => null,
            GearPropType.reqINT => null,
            GearPropType.reqLUK => null,
            GearPropType.reqJob => null,
            GearPropType.reqPOP => null,
            GearPropType.reqSpecJob => null,
            GearPropType.reqWeekDay => null,
            GearPropType.grade => null,
            GearPropType.only => null,
            // GearPropType.notSale => null,
            // GearPropType.dropBlock => null,
            GearPropType.tradeBlock => null,
            GearPropType.accountSharable => null,
            GearPropType.onlyEquip => null,
            GearPropType.tradeAvailable => null,
            GearPropType.equipTradeBlock => null,
            GearPropType.sharableOnce => null,
            GearPropType.notExtend => null,
            GearPropType.epicItem => null,
            GearPropType.charismaEXP => null,
            GearPropType.senseEXP => null,
            GearPropType.insightEXP => null,
            GearPropType.willEXP => null,
            GearPropType.craftEXP => null,
            GearPropType.charmEXP => null,
            GearPropType.bossReward => null,
            GearPropType.cashForceCharmExp => null,
            GearPropType.accountShareTag => null,
            GearPropType.noPotential => null,
            GearPropType.fixedPotential => null,
            GearPropType.timeLimited => null,
            GearPropType.specialGrade => null,
            GearPropType.fixedGrade => null,
            GearPropType.unchangeable => null,
            GearPropType.superiorEqp => null,
            GearPropType.incPQEXPr => null,
            GearPropType.limitBreak => null,
            GearPropType.nActivatedSocket => null,
            GearPropType.jokerToSetItem => null,
            GearPropType.medalTag => null,
            GearPropType.ringOptionSkill => null,
            GearPropType.ringOptionSkillLv => null,
            GearPropType.abilityTimeLimited => null,
            GearPropType.blockGoldHammer => null,
            GearPropType.exceptUpgrade => null,
            GearPropType.colorvar => null,
            GearPropType.noMoveToLocker => null,
            GearPropType.onlyUpgrade => null,
            GearPropType.cantRepair => null,
            GearPropType.noPetEquipStatMoveItem => null,
            GearPropType.BTSLabel => null,
            GearPropType.BLACKPINKLabel => null,
            GearPropType.android => null,
            GearPropType.noLookChange => null,
            GearPropType.tucIgnoreForPotential => null,
            GearPropType.Etuc => null,
            GearPropType.CuttableCount => null,
            GearPropType.exUpgradeBlock => null,
            GearPropType.exUpgradeChangeBlock => null,
            GearPropType.gatherTool_incSkillLevel => null,
            GearPropType.gatherTool_incSpeed => null,
            GearPropType.gatherTool_incNum => null,
            GearPropType.gatherTool_reqSkillLevel => null,
            _ => throw new NotImplementedException("Unknown GearPropType: " + propType),
        };
    }
}